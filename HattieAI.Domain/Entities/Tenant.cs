using HattieAI.Domain.Common;

namespace HattieAI.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Client Name is required")]
        public string Name { get; set; } = string.Empty;
        public string SystemInstruction { get; set; } = "You are a helpful AI assistant.";
        public string KnowledgeBaseText { get; set; } = string.Empty;
    }
}
