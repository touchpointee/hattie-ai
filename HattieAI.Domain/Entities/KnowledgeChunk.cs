using HattieAI.Domain.Common;

namespace HattieAI.Domain.Entities
{
    public class KnowledgeChunk : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public float[]? Embedding { get; set; } // Nullable if embedding fails or is pending
        
        // Navigation (Optional but good for cleanup)
         public Tenant? Tenant { get; set; }
    }
}
