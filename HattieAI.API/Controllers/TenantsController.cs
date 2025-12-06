using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HattieAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly HattieDbContext _context;

        public TenantsController(HattieDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetTenants()
        {
            // Admin only usually, but for now we list all
            return await _context.Tenants.IgnoreQueryFilters().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetTenant(Guid id)
        {
            var tenant = await _context.Tenants
                .Include(t => t.SupportedLanguages)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tenant == null)
            {
                return NotFound();
            }

            // Project to DTO to avoid circular reference (Tenant -> Language -> Tenant)
            return new
            {
                tenant.Name,
                tenant.TenantId,
                SupportedLanguages = tenant.SupportedLanguages.Select(l => new 
                {
                    l.Code,
                    l.Name
                }).ToList()
            };
        }

        [HttpPost]
        public async Task<ActionResult<Tenant>> CreateTenant(Tenant tenant)
        {
            // Ensure ID is generated
            if (tenant.Id == Guid.Empty) tenant.Id = Guid.NewGuid();
            
            // For Tenant creation, we might need to bypass the tenant filter or set the TenantId explicitly
            // Since Tenant entity is the root, its TenantId is usually its own ID or system
            tenant.TenantId = tenant.Id.ToString(); 

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTenant", new { id = tenant.Id }, tenant);
        }
    }
}
