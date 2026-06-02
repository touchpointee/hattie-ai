using HattieAI.Domain.Common;

namespace HattieAI.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Client Name is required")]
        public string Name { get; set; } = string.Empty;
        public string SystemInstruction { get; set; } = "You are a helpful AI assistant.";
        public ICollection<Language> SupportedLanguages { get; set; } = new List<Language>();
        public string KnowledgeBaseText { get; set; } = string.Empty;
        public List<string> AllowedOrigins { get; set; } = new();
        public string WelcomeMessage { get; set; } = "Hello! I am your AI Assistant.";
        public string ContactEmail { get; set; } = string.Empty;
        public int MaxTokensPerSession { get; set; } = 2000;
        public bool IsWebsiteChatbotEnabled { get; set; } = true;
        public bool IsWhatsAppEnabled { get; set; } = false;
    }
}
