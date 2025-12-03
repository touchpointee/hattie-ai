using System;

namespace HattieAI.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string TenantId { get; set; } = string.Empty; // For multi-tenancy isolation
    }
}
