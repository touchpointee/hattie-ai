using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HattieAI.Infrastructure.AI
{
    public class LeadExtractionService
    {
        private readonly GroqBroker _groqBroker;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LeadExtractionService> _logger;

        public LeadExtractionService(GroqBroker groqBroker, IServiceProvider serviceProvider, ILogger<LeadExtractionService> logger)
        {
            _groqBroker = groqBroker;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task ExtractAndSaveLeadAsync(Guid sessionId, string tenantId)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<HattieDbContext>();
                
                dbContext.CurrentTenantId = tenantId;

                var session = await dbContext.ChatSessions
                    .FirstOrDefaultAsync(s => s.Id == sessionId);

                if (session == null)
                {
                    _logger.LogWarning("LeadExtractionService: Session {SessionId} not found.", sessionId);
                    return;
                }

                // Get the tenant configuration
                var tenant = await dbContext.Tenants
                    .FirstOrDefaultAsync(t => t.Id == Guid.Parse(tenantId));

                if (tenant == null || !tenant.IsLeadCollectionEnabled)
                {
                    _logger.LogInformation("LeadExtractionService: Lead collection is disabled for tenant {TenantId}.", tenantId);
                    return;
                }

                // Load messages for this session
                var messages = await dbContext.ChatMessages
                    .Where(m => m.ChatSessionId == sessionId)
                    .OrderBy(m => m.CreatedAt)
                    .Take(15) // Extract from last 15 messages
                    .ToListAsync();

                if (!messages.Any()) return;

                var chatHistory = string.Join("\n", messages.Select(m => $"{m.Role}: {m.Content}"));

                var extractionPrompt = $@"You are a precise data extraction assistant. Analyze the conversation history and extract lead details.
The tenant's specific lead collection instructions are:
""{tenant.LeadCollectionInstruction}""

Your response MUST be a single raw JSON object only. Do NOT include markdown code blocks like ```json or any introductory/concluding text.
Return JSON in this format:
{{
  ""name"": ""extracted name or null"",
  ""email"": ""extracted email or null"",
  ""phone"": ""extracted phone or null"",
  ""company"": ""extracted company name or null"",
  ""notes"": ""brief summary of customer's request/interests or null""
}}";

                // Call Groq to extract details
                var jsonResult = await _groqBroker.GenerateResponseAsync(
                    extractionPrompt,
                    "No additional context.",
                    chatHistory,
                    "Extract the lead contact details (name, email, phone, company, notes) as JSON."
                );

                _logger.LogInformation("LeadExtractionService Result: {Result}", jsonResult);

                // Clean the response if it has markdown formatting
                var cleanJson = jsonResult.Trim();
                if (cleanJson.StartsWith("```"))
                {
                    var lines = cleanJson.Split('\n');
                    cleanJson = string.Join("\n", lines.Skip(1).Take(lines.Length - 2)).Trim();
                    if (cleanJson.StartsWith("json"))
                    {
                        cleanJson = cleanJson.Substring(4).Trim();
                    }
                }

                LeadData? leadData = null;
                try
                {
                    leadData = JsonSerializer.Deserialize<LeadData>(cleanJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "LeadExtractionService: JSON parsing failed. Cleaned JSON: {CleanJson}", cleanJson);
                }

                if (leadData == null) return;

                // Check if we extracted any useful lead details
                bool hasData = !string.IsNullOrEmpty(leadData.Name) || 
                               !string.IsNullOrEmpty(leadData.Email) || 
                               !string.IsNullOrEmpty(leadData.Phone) ||
                               !string.IsNullOrEmpty(leadData.Company);

                if (!hasData)
                {
                    _logger.LogInformation("LeadExtractionService: No lead details found in session {SessionId}.", sessionId);
                    return;
                }

                // Check if a lead already exists for this session
                var lead = await dbContext.Leads
                    .FirstOrDefaultAsync(l => l.ChatSessionId == sessionId);

                if (lead == null)
                {
                    lead = new Lead
                    {
                        Id = Guid.NewGuid(),
                        TenantId = tenantId,
                        ChatSessionId = sessionId,
                        Stage = "New",
                        Source = session.Channel == "WhatsApp" ? "WhatsApp" : "Website Chatbot",
                        CreatedAt = DateTime.UtcNow
                    };
                    dbContext.Leads.Add(lead);
                }

                // Update lead details (only overwrite if not null/empty in the extracted data)
                if (!string.IsNullOrWhiteSpace(leadData.Name) && leadData.Name != "null")
                {
                    lead.Name = leadData.Name.Trim();
                    if (string.IsNullOrWhiteSpace(session.ContactName))
                    {
                        session.ContactName = lead.Name;
                    }
                }
                else if (string.IsNullOrWhiteSpace(lead.Name))
                {
                    lead.Name = session.ContactName ?? session.Title ?? "Unknown Lead";
                }

                if (!string.IsNullOrWhiteSpace(leadData.Email) && leadData.Email != "null")
                {
                    lead.Email = leadData.Email.Trim();
                }

                if (!string.IsNullOrWhiteSpace(leadData.Phone) && leadData.Phone != "null")
                {
                    lead.Phone = leadData.Phone.Trim();
                }
                else if (string.IsNullOrWhiteSpace(lead.Phone) && session.Channel == "WhatsApp")
                {
                    lead.Phone = session.UserId;
                }

                if (!string.IsNullOrWhiteSpace(leadData.Company) && leadData.Company != "null")
                {
                    lead.Company = leadData.Company.Trim();
                }

                if (!string.IsNullOrWhiteSpace(leadData.Notes) && leadData.Notes != "null")
                {
                    lead.Notes = leadData.Notes.Trim();
                }

                await dbContext.SaveChangesAsync();
                _logger.LogInformation("LeadExtractionService: Successfully saved/updated lead {LeadId}", lead.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LeadExtractionService: Error during extraction.");
            }
        }

        private class LeadData
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Company { get; set; }
            public string? Notes { get; set; }
        }
    }
}
