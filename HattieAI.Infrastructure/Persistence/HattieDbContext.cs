using HattieAI.Domain.Common;
using HattieAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;

namespace HattieAI.Infrastructure.Persistence
{
    public class HattieDbContext : DbContext
    {
        private string _currentTenantId = string.Empty;
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

        public DbSet<Tenant> Tenants { get; set; } = null!;
        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<ChatSession> ChatSessions { get; set; } = null!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;

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
                    var tenantId = Expression.Property(Expression.Constant(this), nameof(CurrentTenantId));
                    var filter = Expression.Equal(property, tenantId);
                    var lambda = Expression.Lambda(filter, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }


            // Seed Languages (User Requested List)
            var languages = new List<Language>
            {
                new Language { Code = "en", Name = "English" },
                new Language { Code = "zh-CN", Name = "Mandarin Chinese" },
                new Language { Code = "hi", Name = "Hindi" },
                new Language { Code = "es", Name = "Spanish" },
                new Language { Code = "ar", Name = "Arabic" },
                new Language { Code = "fr", Name = "French" },
                new Language { Code = "bn", Name = "Bengali" },
                new Language { Code = "pt", Name = "Portuguese" },
                new Language { Code = "ur", Name = "Urdu" },
                new Language { Code = "id", Name = "Indonesian" },
                new Language { Code = "zh-TW", Name = "Standard Chinese (Traditional)" }, // Mapping for Non-Mandarin/Traditional
                new Language { Code = "ru", Name = "Russian" },
                new Language { Code = "ja", Name = "Japanese" },
                new Language { Code = "pa", Name = "Punjabi" },
                new Language { Code = "de", Name = "German" },
                new Language { Code = "jv", Name = "Javanese" },
                new Language { Code = "ko", Name = "Korean" },
                new Language { Code = "te", Name = "Telugu" },
                new Language { Code = "vi", Name = "Vietnamese" },
                new Language { Code = "mr", Name = "Marathi" },
                new Language { Code = "tr", Name = "Turkish" },
                new Language { Code = "ta", Name = "Tamil" },
                new Language { Code = "it", Name = "Italian" },
                new Language { Code = "yue", Name = "Yue Chinese (Cantonese)" },
                new Language { Code = "th", Name = "Thai" },
                new Language { Code = "gu", Name = "Gujarati" },
                new Language { Code = "kn", Name = "Kannada" },
                new Language { Code = "fa", Name = "Persian (Farsi)" },
                new Language { Code = "pl", Name = "Polish" },
                new Language { Code = "uk", Name = "Ukrainian" },
                new Language { Code = "ml", Name = "Malayalam" },
                new Language { Code = "ms", Name = "Malay" },
                new Language { Code = "ha", Name = "Hausa" },
                new Language { Code = "my", Name = "Burmese" },
                new Language { Code = "su", Name = "Sundanese" },
                new Language { Code = "bho", Name = "Bhojpuri" },
                new Language { Code = "nl", Name = "Dutch" },
                new Language { Code = "yo", Name = "Yoruba" },
                new Language { Code = "or", Name = "Odia (Oriya)" },
                new Language { Code = "sd", Name = "Sindhi" },
                new Language { Code = "am", Name = "Amharic" },
                new Language { Code = "mai", Name = "Maithili" },
                new Language { Code = "uz", Name = "Uzbek" },
                new Language { Code = "skr", Name = "Saraiki" },
                new Language { Code = "ne", Name = "Nepali" },
                new Language { Code = "si", Name = "Sinhala" },
                new Language { Code = "km", Name = "Khmer" },
                new Language { Code = "az", Name = "Azerbaijani" },
                new Language { Code = "ro", Name = "Romanian" },
                new Language { Code = "ceb", Name = "Cebuano" }
            };

            modelBuilder.Entity<Language>().HasData(languages);
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
