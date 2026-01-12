using Microsoft.AspNetCore.Cors.Infrastructure;
using HattieAI.Infrastructure.Persistence;
using HattieAI.Infrastructure.AI;
using HattieAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HattieAI.API.Services
{
    public class TenantCorsPolicyProvider : ICorsPolicyProvider
    {
        private readonly DefaultCorsPolicyProvider _defaultProvider;

        public TenantCorsPolicyProvider(Microsoft.Extensions.Options.IOptions<CorsOptions> options)
        {
            _defaultProvider = new DefaultCorsPolicyProvider(options);
        }

        public async Task<CorsPolicy?> GetPolicyAsync(HttpContext context, string? policyName)
        {
            if (policyName != "DynamicTenantPolicy")
            {
                return await _defaultProvider.GetPolicyAsync(context, policyName);
            }

            var policyBuilder = new CorsPolicyBuilder()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();

            // Resolve services from the current request scope
            var tenantProvider = context.RequestServices.GetService<ITenantProvider>();
            var dbContext = context.RequestServices.GetService<HattieDbContext>();

            if (tenantProvider != null && dbContext != null)
            {
                var tenantIdString = tenantProvider.TenantId;
                if (!string.IsNullOrEmpty(tenantIdString) && Guid.TryParse(tenantIdString, out var tenantGuid))
                {
                    // Check cache or DB
                    // For performance, we should cache this. But for now, direct DB for correctness as per plan.
                    // We need to use a clean context or careful with tracking if standard pipeline uses it.
                    // Actually, context.RequestServices gives us the scoped context which is fine.
                    
                    var tenant = await dbContext.Tenants
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(t => t.Id == tenantGuid);

                    if (tenant != null && tenant.AllowedOrigins != null)
                    {
                        if (tenant.AllowedOrigins.Count > 0)
                        {
                            policyBuilder.WithOrigins(tenant.AllowedOrigins.ToArray());
                        }
                        else 
                        {
                           // Fallback: If no origins defined, maybe allow none or all? 
                           // Usage implies we want to RESTRICT.
                           // But if list is empty, maybe we block all external?
                           // Let's allow localhost for dev?
                           // Or purely block.
                           // User request: "Work CORS", "Add website list".
                           // If list empty -> Block.
                        }
                    }
                }
            }
            
            // Allow Localhost for development convenience
            // policyBuilder.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback);

            return policyBuilder.Build();
        }
    }
}
