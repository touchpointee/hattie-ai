using HattieAI.Domain.Common;

namespace HattieAI.Domain.Entities
{
    public class ApiIntegration : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = "POST";
        public string Url { get; set; } = string.Empty;
        public string AuthType { get; set; } = "None"; // None, BearerToken, ApiKey, BasicAuth
        public string AuthValue { get; set; } = string.Empty; // Encrypted
        public string AuthHeaderName { get; set; } = string.Empty; // For ApiKey type
        public string Headers { get; set; } = "{}"; // JSON string of extra headers
        public string RequestBodyTemplate { get; set; } = "{}"; // JSON with {{placeholders}}
        public string ParameterSchema { get; set; } = "[]"; // JSON array of param definitions
        public string Direction { get; set; } = "Push"; // Push or Pull
        public bool IsActive { get; set; } = true;
        public int DailyCallLimit { get; set; } = 100;
        public int DailyCallCount { get; set; } = 0;
        public DateTime CallCountResetDate { get; set; } = DateTime.UtcNow;
    }
}
