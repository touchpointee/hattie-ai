using HattieAI.Domain.Common;

namespace HattieAI.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public Guid ChatSessionId { get; set; }
        public string Role { get; set; } = "user"; // "user" or "model"
        public string Content { get; set; } = string.Empty;
        
        // Navigation property
        // public ChatSession ChatSession { get; set; } 
    }
}
