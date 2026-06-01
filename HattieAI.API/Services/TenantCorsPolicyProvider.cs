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
                    var tenant = await dbContext.Tenants
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(t => t.Id == tenantGuid);

                    if (tenant != null)
                    {
                        var allowedOrigins = tenant.AllowedOrigins ?? new List<string>();

                        // Dynamic matching logic
                        policyBuilder.SetIsOriginAllowed(origin =>
                        {
                            try
                            {
                                var originUri = new Uri(origin);
                                
                                // 1. Always allow localhost and loopback for local development
                                if (originUri.IsLoopback || originUri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                                {
                                    return true;
                                }

                                // 2. If client hasn't configured allowed websites, allow all origins by default
                                // to ensure onboarding is frictionless and chatbot works initially out-of-the-box.
                                if (allowedOrigins.Count == 0 || allowedOrigins.Contains("*"))
                                {
                                    return true;
                                }

                                // 3. Match against configured websites flexibly
                                foreach (var allowed in allowedOrigins)
                                {
                                    var cleanedAllowed = allowed.Trim().TrimEnd('/');
                                    
                                    // If database entry includes scheme
                                    if (cleanedAllowed.Contains("://"))
                                    {
                                        var allowedUri = new Uri(cleanedAllowed);
                                        // Match host (e.g. example.com == example.com)
                                        if (originUri.Host.Equals(allowedUri.Host, StringComparison.OrdinalIgnoreCase))
                                            return true;
                                        
                                        // Match domain ending for subdomain wildcards (e.g. app.example.com ends with .example.com)
                                        if (originUri.Host.EndsWith("." + allowedUri.Host, StringComparison.OrdinalIgnoreCase))
                                            return true;
                                    }
                                    else
                                    {
                                        // If database entry is just domain (e.g. "example.com")
                                        if (originUri.Host.Equals(cleanedAllowed, StringComparison.OrdinalIgnoreCase) ||
                                            originUri.Host.EndsWith("." + cleanedAllowed, StringComparison.OrdinalIgnoreCase))
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                // Fall back to denying on exception
                            }

                            return false;
                        });
                    }
                }
                else
                {
                    // Fallback to allowing localhost when tenantId is not immediately available in preflight
                    policyBuilder.SetIsOriginAllowed(origin =>
                    {
                        try
                        {
                            var uri = new Uri(origin);
                            return uri.IsLoopback || uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);
                        }
                        catch
                        {
                            return false;
                        }
                    });
                }
            }
            else
            {
                policyBuilder.SetIsOriginAllowed(origin => origin.Contains("localhost") || origin.Contains("127.0.0.1"));
            }

            return policyBuilder.Build();
        }
    }
}
