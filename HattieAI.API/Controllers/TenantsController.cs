using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(Guid id, Tenant tenant)
        {
            if (id != tenant.Id)
            {
                return BadRequest();
            }

            var existingTenant = await _context.Tenants.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == id);
            if (existingTenant == null)
            {
                return NotFound();
            }

            existingTenant.Name = tenant.Name;
            existingTenant.SystemInstruction = tenant.SystemInstruction;
            existingTenant.SupportedLanguages = tenant.SupportedLanguages; // Handled by EF tracking if properly attached

            // Check if KB changed
            if (existingTenant.KnowledgeBaseText != tenant.KnowledgeBaseText)
            {
                existingTenant.KnowledgeBaseText = tenant.KnowledgeBaseText;
                
                // 1. DELETE OLD CHUNKS
                var oldChunks = await _context.KnowledgeChunks.Where(k => k.TenantId == existingTenant.TenantId).ToListAsync();
                _context.KnowledgeChunks.RemoveRange(oldChunks);

                // 2. CHUNK NEW TEXT
                if (!string.IsNullOrEmpty(tenant.KnowledgeBaseText))
                {
                    var chunks = ChunkText(tenant.KnowledgeBaseText, 500); 
                    foreach (var chunkText in chunks)
                    {
                        // 3. EMBED (using simple hash-based embedding)
                        var vector = GenerateSimpleEmbedding(chunkText);
                        
                        // 4. SAVE
                        _context.KnowledgeChunks.Add(new KnowledgeChunk
                        {
                            Id = Guid.NewGuid(),
                            TenantId = existingTenant.TenantId,
                            Content = chunkText,
                            Embedding = vector,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }

            return NoContent();
        }

        private List<string> ChunkText(string text, int maxChunkSize)
        {
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var chunks = new List<string>();
            var currentChunk = "";

            foreach (var line in lines)
            {
                if (currentChunk.Length + line.Length > maxChunkSize)
                {
                    chunks.Add(currentChunk);
                    currentChunk = "";
                }
                currentChunk += line + " ";
            }
            if (!string.IsNullOrWhiteSpace(currentChunk))
            {
                chunks.Add(currentChunk);
            }
            return chunks;
        }

        /// <summary>
        /// Simple hash-based embedding for vector search (Groq doesn't have embeddings API).
        /// </summary>
        private float[] GenerateSimpleEmbedding(string text)
        {
            const int dimensions = 768;
            var embedding = new float[dimensions];
            var words = text.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var word in words)
            {
                var hash = word.GetHashCode();
                var index = Math.Abs(hash) % dimensions;
                embedding[index] += 1.0f;
            }

            // Normalize
            var magnitude = (float)Math.Sqrt(embedding.Sum(x => x * x));
            if (magnitude > 0)
            {
                for (int i = 0; i < dimensions; i++)
                    embedding[i] /= magnitude;
            }

            return embedding;
        }
    }
}
