using HattieAI.Domain.Common;
using System;

namespace HattieAI.Domain.Entities
{
    public class Lead : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Stage { get; set; } = "New"; // "New", "Contacted", "Qualified", "Proposal", "Won", "Lost"
        public string Source { get; set; } = "Website Chatbot"; // "Website Chatbot" or "WhatsApp"
        public Guid? ChatSessionId { get; set; }
    }
}
