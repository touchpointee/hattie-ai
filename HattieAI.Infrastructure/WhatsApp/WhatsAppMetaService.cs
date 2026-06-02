using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HattieAI.Infrastructure.WhatsApp
{
    public class WhatsAppMetaService
    {
        private readonly HttpClient _httpClient;
        private const string MetaApiVersion = "v21.0";
        private const string MetaApiBaseUrl = "https://graph.facebook.com";

        public WhatsAppMetaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MetaPhoneInfo> VerifyPhoneNumberAsync(string phoneNumberId, string accessToken)
        {
            var url = $"{MetaApiBaseUrl}/{MetaApiVersion}/{phoneNumberId}?fields=id,display_phone_number,verified_name,quality_rating";
            
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Meta API returned error: {response.StatusCode} - {errorContent}");
            }

            var contentString = await response.Content.ReadAsStringAsync();
            var info = JsonSerializer.Deserialize<MetaPhoneInfo>(contentString);
            return info ?? throw new Exception("Failed to deserialize Meta API response.");
        }

        public async Task<string> SendTextMessageAsync(string phoneNumberId, string accessToken, string to, string text)
        {
            var url = $"{MetaApiBaseUrl}/{MetaApiVersion}/{phoneNumberId}/messages";

            var body = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = to,
                type = "text",
                text = new { body = text }
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Meta API message send failed: {response.StatusCode} - {errorContent}");
            }

            var contentString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(contentString);
            try
            {
                // Result has shape: { messages: [ { id: "wamid..." } ] }
                return doc.RootElement
                    .GetProperty("messages")[0]
                    .GetProperty("id")
                    .GetString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse Meta send message response: {ex.Message}. Response: {contentString}");
            }
        }
    }

    public class MetaPhoneInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("display_phone_number")]
        public string DisplayPhoneNumber { get; set; } = string.Empty;

        [JsonPropertyName("verified_name")]
        public string VerifiedName { get; set; } = string.Empty;

        [JsonPropertyName("quality_rating")]
        public string QualityRating { get; set; } = string.Empty;
    }
}
