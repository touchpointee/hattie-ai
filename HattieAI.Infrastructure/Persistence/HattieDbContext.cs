using HattieAI.Domain.Common;
using HattieAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace HattieAI.Infrastructure.Persistence
{
    public class HattieDbContext : DbContext
    {
        private string _currentTenantId;
        public string CurrentTenantId 
        { 
            get => _currentTenantId; 
            set => _currentTenantId = value; 
        }

        public HattieDbContext(DbContextOptions<HattieDbContext> options) : base(options)
        {
            // In a real app, this would be injected via a scoped service (ITenantProvider)
            // For now, we might need a way to set this. 
            // We'll assume it's passed or set via a service.
            // For simplicity in this scaffold, we'll leave it empty and rely on manual setting or DI.
        }

        // Constructor for DI with Tenant Provider if we had one. 
        // For now, let's add a property to set it, or rely on a service locator pattern if strictly needed, 
        // but better to inject a service. 
        // Let's assume we have an ITenantProvider.
        
        /*
        public HattieDbContext(DbContextOptions<HattieDbContext> options, ITenantProvider tenantProvider = null) : base(options)
        {
            _currentTenantId = tenantProvider?.TenantId;
        }
        */

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply Global Query Filter for Tenant Isolation
            // We need to apply this to all entities inheriting from BaseEntity, except Tenant itself maybe?
            // Usually Tenant entity is global or system admin managed. 
            // Let's filter everything else.

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType) && entityType.ClrType != typeof(Tenant))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.TenantId));
                    var tenantId = Expression.Constant(_currentTenantId);
                    var filter = Expression.Equal(property, tenantId);
                    var lambda = Expression.Lambda(filter, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (string.IsNullOrEmpty(entry.Entity.TenantId) && !string.IsNullOrEmpty(_currentTenantId))
                        {
                            entry.Entity.TenantId = _currentTenantId;
                        }
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

    public interface ITenantProvider
    {
        string TenantId { get; }
    }
}
