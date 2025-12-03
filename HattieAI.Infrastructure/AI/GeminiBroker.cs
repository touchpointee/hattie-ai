using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HattieAI.Infrastructure.AI
{
    public class GeminiBroker
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiBroker(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public async Task<string> GenerateResponseAsync(string systemInstruction, string knowledgeBase, string chatHistory, string userMessage)
        {
            var prompt = $"{systemInstruction}\n\nContext:\n{knowledgeBase}\n\nChat History:\n{chatHistory}\n\nUser: {userMessage}\nModel:";
            
            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                },
                generationConfig = new
                {
                    temperature = 0.9,
                    topK = 40,
                    topP = 0.95,
                    maxOutputTokens = 1024,
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-lite:generateContent?key={_apiKey}", content);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Gemini API Error: {response.StatusCode}");
                Console.WriteLine($"Error Content: {errorContent}");
                return $"Error calling Gemini API. Status: {response.StatusCode}. Details: {errorContent}";
            }

            var responseString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            // Extract text from response structure
            try 
            {
                return doc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
            }
            catch
            {
                return "Error parsing Gemini response.";
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
