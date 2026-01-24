using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HattieAI.Infrastructure.AI
{
    public class GroqBroker
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string MODEL = "llama-3.1-8b-instant";

        public GroqBroker(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Groq:ApiKey"] ?? "";
        }

        public async Task<string> GenerateResponseAsync(string systemInstruction, string knowledgeBase, string chatHistory, string userMessage)
        {
            var messages = new List<object>
            {
                new { role = "system", content = $"{systemInstruction}\n\nContext:\n{knowledgeBase}" }
            };

            // Add chat history if present
            if (!string.IsNullOrWhiteSpace(chatHistory))
            {
                foreach (var line in chatHistory.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.StartsWith("user:"))
                        messages.Add(new { role = "user", content = line.Substring(5).Trim() });
                    else if (line.StartsWith("model:"))
                        messages.Add(new { role = "assistant", content = line.Substring(6).Trim() });
                }
            }

            // Add current user message
            messages.Add(new { role = "user", content = userMessage });

            var requestBody = new
            {
                model = MODEL,
                messages = messages,
                temperature = 0.9,
                max_tokens = 1024,
                top_p = 0.95
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            Console.WriteLine($"[GroqBroker] Using Model: {MODEL}");
            var response = await _httpClient.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Groq API Error: {response.StatusCode}");
                Console.WriteLine($"Error Content: {errorContent}");
                return $"Error calling Groq API. Status: {response.StatusCode}. Details: {errorContent}";
            }

            var responseString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);

            try
            {
                return doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString() ?? "";
            }
            catch
            {
                return "Error parsing Groq response.";
            }
        }

        public async IAsyncEnumerable<string> GenerateResponseStreamAsync(string systemInstruction, string knowledgeBase, string chatHistory, string userMessage)
        {
            var response = await GenerateResponseAsync(systemInstruction, knowledgeBase, chatHistory, userMessage);

            foreach (var character in response)
            {
                yield return character.ToString();
                await Task.Delay(5); // Simulate typing effect
            }
        }
    }
}
