using HattieAI.Domain.Common;

namespace HattieAI.Domain.Entities
{
    public class WhatsAppAutomationRule : BaseEntity
    {
        public string TriggerKeyword { get; set; } = string.Empty;
        public string MatchType { get; set; } = "Exact"; // "Exact" or "Contains"
        public string ReplyText { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
