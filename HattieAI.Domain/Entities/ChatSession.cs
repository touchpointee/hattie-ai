using HattieAI.Domain.Common;
using System.Collections.Generic;

namespace HattieAI.Domain.Entities
{
    public class ChatSession : BaseEntity
    {
        public string UserId { get; set; } = string.Empty; // Or link to AppUser if strict FK needed
        public string Title { get; set; } = "New Chat";
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
