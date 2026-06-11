using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Persistence;
using HattieAI.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HattieAI.Infrastructure.AI
{
    public class ApiExecutionService
    {
        private readonly GroqBroker _groqBroker;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ApiExecutionService> _logger;
        private static readonly TimeSpan HttpTimeout = TimeSpan.FromSeconds(10);

        public ApiExecutionService(
            GroqBroker groqBroker,
            IHttpClientFactory httpClientFactory,
            IServiceProvider serviceProvider,
            ILogger<ApiExecutionService> logger)
        {
            _groqBroker = groqBroker;
            _httpClientFactory = httpClientFactory;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// Evaluates whether any configured API should be called based on the conversation,
        /// executes the call if needed, and returns the result.
        /// </summary>
        public async Task<ApiCallResult?> EvaluateAndExecuteAsync(
            string userMessage,
            string chatHistory,
            List<ApiIntegration> integrations,
            string encryptionKey,
            string tenantId)
        {
            if (integrations == null || !integrations.Any())
                return null;

            try
            {
                // Step 1: Ask the LLM which API (if any) should be called
                var decision = await GetApiDecisionAsync(userMessage, chatHistory, integrations);

                if (decision == null || !decision.ShouldCall)
                {
                    _logger.LogInformation("[ApiExecution] LLM decided no API call is needed.");
                    return null;
                }

                // Step 2: Find the matching integration
                var integration = integrations.FirstOrDefault(i =>
                    string.Equals(i.Name, decision.ApiName, StringComparison.OrdinalIgnoreCase));

                if (integration == null)
                {
                    _logger.LogWarning("[ApiExecution] LLM referenced API '{ApiName}' but it was not found.", decision.ApiName);
                    return null;
                }

                // Step 3: Check rate limit
                if (!CheckAndIncrementRateLimit(integration))
                {
                    _logger.LogWarning("[ApiExecution] Rate limit exceeded for API '{ApiName}'.", integration.Name);
                    return new ApiCallResult
                    {
                        IntegrationName = integration.Name,
                        Direction = integration.Direction,
                        StatusCode = 429,
                        ResponseBody = "",
                        IsSuccess = false,
                        FriendlyError = "This service is temporarily busy. Please try again later."
                    };
                }

                // Step 4: Execute the API call
                var result = await ExecuteApiCallAsync(integration, decision.Parameters, encryptionKey);

                // Step 5: Persist rate limit counter
                await PersistRateLimitAsync(integration, tenantId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ApiExecution] Error during API evaluation/execution.");
                return new ApiCallResult
                {
                    IntegrationName = "Unknown",
                    Direction = "Push",
                    StatusCode = 0,
                    ResponseBody = "",
                    IsSuccess = false,
                    FriendlyError = "We're experiencing a temporary issue processing your request. Please try again shortly."
                };
            }
        }

        private async Task<ApiDecision?> GetApiDecisionAsync(
            string userMessage,
            string chatHistory,
            List<ApiIntegration> integrations)
        {
            // Build the API descriptions for the LLM
            var apiDescriptions = new StringBuilder();
            for (int i = 0; i < integrations.Count; i++)
            {
                var api = integrations[i];
                apiDescriptions.AppendLine($"{i + 1}. Name: \"{api.Name}\" | Direction: {api.Direction} | Description: \"{api.Description}\"");

                // Parse parameter schema
                try
                {
                    var parameters = JsonSerializer.Deserialize<List<ParameterDef>>(api.ParameterSchema,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (parameters != null && parameters.Any())
                    {
                        var paramList = string.Join(", ",
                            parameters.Select(p => $"{p.Name} ({p.Type}, {(p.Required ? "required" : "optional")}: {p.Description})"));
                        apiDescriptions.AppendLine($"   Parameters: {paramList}");
                    }
                }
                catch
                {
                    // Ignore parse errors for parameter schema
                }
            }

            var decisionPrompt = $@"You are an API routing assistant. Given these available API integrations and the conversation below, decide if any API should be called RIGHT NOW based on the conversation context.

IMPORTANT RULES:
- Only call an API if the user has provided enough information to fill the REQUIRED parameters.
- For ""Push"" APIs, only call when the user has confirmed or clearly expressed intent (e.g., confirmed an order).
- For ""Pull"" APIs, call when the user asks a question that the API can answer (e.g., checking availability, getting a price).
- If not enough info is available yet, do NOT call the API.

Available APIs:
{apiDescriptions}

Conversation:
{chatHistory}
user: {userMessage}

Your response MUST be a single raw JSON object only. No markdown, no explanation.
If an API should be called:
{{""should_call"": true, ""api_name"": ""exact API name"", ""parameters"": {{""param1"": ""value1"", ""param2"": ""value2""}}}}
If no API should be called:
{{""should_call"": false}}";

            var jsonResult = await _groqBroker.GenerateResponseAsync(
                "You are a precise JSON-only API routing assistant. You MUST respond with valid JSON only. No markdown, no explanation text.",
                "",
                "",
                decisionPrompt);

            _logger.LogInformation("[ApiExecution] LLM Decision: {Result}", jsonResult);

            // Clean the response
            var cleanJson = CleanJsonResponse(jsonResult);

            try
            {
                var decision = JsonSerializer.Deserialize<ApiDecision>(cleanJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return decision;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ApiExecution] Failed to parse LLM decision JSON: {Json}", cleanJson);
                return null;
            }
        }

        private async Task<ApiCallResult> ExecuteApiCallAsync(
            ApiIntegration integration,
            Dictionary<string, JsonElement>? parameters,
            string encryptionKey)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = HttpTimeout;

                // Build the request
                var url = integration.Url;
                HttpRequestMessage request;

                // For GET requests, append parameters as query string
                if (integration.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    if (parameters != null && parameters.Any())
                    {
                        var queryParams = string.Join("&",
                            parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(GetStringValue(p.Value))}"));
                        url = url.Contains("?") ? $"{url}&{queryParams}" : $"{url}?{queryParams}";
                    }
                    request = new HttpRequestMessage(HttpMethod.Get, url);
                }
                else
                {
                    var method = integration.HttpMethod.ToUpperInvariant() switch
                    {
                        "POST" => HttpMethod.Post,
                        "PUT" => HttpMethod.Put,
                        "PATCH" => HttpMethod.Patch,
                        "DELETE" => HttpMethod.Delete,
                        _ => HttpMethod.Post
                    };

                    request = new HttpRequestMessage(method, url);

                    // Build body from template
                    var body = integration.RequestBodyTemplate ?? "{}";
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            var placeholder = $"{{{{{param.Key}}}}}";
                            var value = GetStringValue(param.Value);
                            body = body.Replace(placeholder, value);
                        }
                    }

                    // Clean up any remaining unfilled placeholders
                    body = Regex.Replace(body, @"\{\{[^}]+\}\}", "\"\"");

                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }

                // Add authentication
                if (!string.IsNullOrEmpty(integration.AuthType) && integration.AuthType != "None" && !string.IsNullOrEmpty(integration.AuthValue))
                {
                    var decryptedAuth = "";
                    try
                    {
                        decryptedAuth = EncryptionHelper.Decrypt(integration.AuthValue, encryptionKey);
                    }
                    catch
                    {
                        _logger.LogWarning("[ApiExecution] Failed to decrypt auth for API '{Name}'.", integration.Name);
                    }

                    switch (integration.AuthType)
                    {
                        case "BearerToken":
                            request.Headers.Add("Authorization", $"Bearer {decryptedAuth}");
                            break;
                        case "ApiKey":
                            var headerName = string.IsNullOrEmpty(integration.AuthHeaderName) ? "X-API-Key" : integration.AuthHeaderName;
                            request.Headers.Add(headerName, decryptedAuth);
                            break;
                        case "BasicAuth":
                            var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(decryptedAuth));
                            request.Headers.Add("Authorization", $"Basic {encodedAuth}");
                            break;
                    }
                }

                // Add extra headers
                if (!string.IsNullOrEmpty(integration.Headers) && integration.Headers != "{}")
                {
                    try
                    {
                        var extraHeaders = JsonSerializer.Deserialize<Dictionary<string, string>>(integration.Headers);
                        if (extraHeaders != null)
                        {
                            foreach (var header in extraHeaders)
                            {
                                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                            }
                        }
                    }
                    catch
                    {
                        // Ignore header parse errors
                    }
                }

                _logger.LogInformation("[ApiExecution] Calling {Method} {Url} for API '{Name}'",
                    integration.HttpMethod, integration.Url, integration.Name);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("[ApiExecution] Response: {StatusCode} for API '{Name}'",
                    (int)response.StatusCode, integration.Name);

                return new ApiCallResult
                {
                    IntegrationName = integration.Name,
                    Direction = integration.Direction,
                    StatusCode = (int)response.StatusCode,
                    ResponseBody = responseBody,
                    IsSuccess = response.IsSuccessStatusCode,
                    FriendlyError = response.IsSuccessStatusCode ? null :
                        "We encountered a temporary issue while processing your request. Let me help you another way."
                };
            }
            catch (TaskCanceledException)
            {
                _logger.LogWarning("[ApiExecution] HTTP request timed out for API '{Name}'.", integration.Name);
                return new ApiCallResult
                {
                    IntegrationName = integration.Name,
                    Direction = integration.Direction,
                    StatusCode = 408,
                    ResponseBody = "",
                    IsSuccess = false,
                    FriendlyError = "The service is taking longer than expected. Please try again in a moment."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ApiExecution] HTTP request failed for API '{Name}'.", integration.Name);
                return new ApiCallResult
                {
                    IntegrationName = integration.Name,
                    Direction = integration.Direction,
                    StatusCode = 0,
                    ResponseBody = "",
                    IsSuccess = false,
                    FriendlyError = "We're experiencing a temporary issue. Please try again shortly."
                };
            }
        }

        private bool CheckAndIncrementRateLimit(ApiIntegration integration)
        {
            // Reset counter if it's a new day
            if (integration.CallCountResetDate.Date < DateTime.UtcNow.Date)
            {
                integration.DailyCallCount = 0;
                integration.CallCountResetDate = DateTime.UtcNow;
            }

            if (integration.DailyCallCount >= integration.DailyCallLimit)
                return false;

            integration.DailyCallCount++;
            return true;
        }

        private async Task PersistRateLimitAsync(ApiIntegration integration, string tenantId)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<HattieDbContext>();
                dbContext.CurrentTenantId = tenantId;

                var dbIntegration = await dbContext.ApiIntegrations
                    .FirstOrDefaultAsync(a => a.Id == integration.Id);

                if (dbIntegration != null)
                {
                    dbIntegration.DailyCallCount = integration.DailyCallCount;
                    dbIntegration.CallCountResetDate = integration.CallCountResetDate;
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ApiExecution] Failed to persist rate limit for API '{Name}'.", integration.Name);
            }
        }

        private static string GetStringValue(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString() ?? "",
                JsonValueKind.Number => element.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "",
                _ => element.GetRawText()
            };
        }

        private static string CleanJsonResponse(string response)
        {
            var clean = response.Trim();
            if (clean.StartsWith("```"))
            {
                var lines = clean.Split('\n');
                clean = string.Join("\n", lines.Skip(1).Take(lines.Length - 2)).Trim();
                if (clean.StartsWith("json"))
                    clean = clean.Substring(4).Trim();
            }
            return clean;
        }
    }

    public class ApiCallResult
    {
        public string IntegrationName { get; set; } = string.Empty;
        public string Direction { get; set; } = "Push";
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string? FriendlyError { get; set; }
    }

    internal class ApiDecision
    {
        public bool ShouldCall { get; set; }
        public string? ApiName { get; set; }
        public Dictionary<string, JsonElement>? Parameters { get; set; }
    }

    internal class ParameterDef
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "string";
        public bool Required { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
