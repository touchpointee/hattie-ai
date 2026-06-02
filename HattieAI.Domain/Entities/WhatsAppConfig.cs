using HattieAI.Domain.Common;
using System;

namespace HattieAI.Domain.Entities
{
    public class WhatsAppConfig : BaseEntity
    {
        public string PhoneNumberId { get; set; } = string.Empty;
        public string? WabaId { get; set; }
        public string AccessToken { get; set; } = string.Empty; // Encrypted (AES-GCM)
        public string VerifyToken { get; set; } = string.Empty; // Encrypted (AES-GCM)
        public string Status { get; set; } = "disconnected"; // connected or disconnected
        public DateTime? ConnectedAt { get; set; }
    }
}
