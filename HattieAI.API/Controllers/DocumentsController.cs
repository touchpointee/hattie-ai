using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Documents;
using HattieAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HattieAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly HattieDbContext _context;
        private readonly DocumentBroker _documentBroker;

        public DocumentsController(HattieDbContext context, DocumentBroker documentBroker)
        {
            _context = context;
            _documentBroker = documentBroker;
        }

        [HttpPost("upload/{tenantId}")]
        public async Task<IActionResult> UploadDocument(Guid tenantId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // 1. Extract Text
            string extractedText;
            using (var stream = file.OpenReadStream())
            {
                extractedText = _documentBroker.ExtractText(stream);
            }

            // 2. Update Tenant Knowledge Base
            var tenant = await _context.Tenants.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == tenantId);
            if (tenant == null)
                return NotFound($"Tenant with ID {tenantId} not found.");

            if (!string.IsNullOrEmpty(tenant.KnowledgeBaseText))
            {
                tenant.KnowledgeBaseText += "\n\n--- NEW DOCUMENT ---\n\n";
            }
            tenant.KnowledgeBaseText += extractedText;

            // Rebuild chunks
            var oldChunks = await _context.KnowledgeChunks.Where(k => k.TenantId == tenant.TenantId).ToListAsync();
            _context.KnowledgeChunks.RemoveRange(oldChunks);

            if (!string.IsNullOrEmpty(tenant.KnowledgeBaseText))
            {
                var chunks = ChunkText(tenant.KnowledgeBaseText, 700);
                foreach (var chunkText in chunks)
                {
                    _context.KnowledgeChunks.Add(new KnowledgeChunk
                    {
                        Id = Guid.NewGuid(),
                        TenantId = tenant.TenantId,
                        Content = chunkText,
                        Embedding = GenerateSimpleEmbedding(chunkText),
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Document processed and knowledge base updated.", TenantId = tenantId, TextLength = extractedText.Length });
        }

        private static List<string> ChunkText(string text, int maxChunkSize)
        {
            var paragraphs = text.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            var chunks = new List<string>();
            var currentChunk = "";

            foreach (var paragraph in paragraphs)
            {
                var cleanParagraph = paragraph.Trim();
                if (string.IsNullOrWhiteSpace(cleanParagraph))
                    continue;

                if (currentChunk.Length + cleanParagraph.Length > maxChunkSize && !string.IsNullOrWhiteSpace(currentChunk))
                {
                    chunks.Add(currentChunk.Trim());
                    currentChunk = "";
                }

                currentChunk += cleanParagraph + "\n\n";
            }

            if (!string.IsNullOrWhiteSpace(currentChunk))
                chunks.Add(currentChunk.Trim());

            return chunks;
        }

        private static float[] GenerateSimpleEmbedding(string text)
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

            var magnitude = (float)Math.Sqrt(embedding.Sum(x => x * x));
            if (magnitude > 0)
            {
                for (var i = 0; i < dimensions; i++)
                    embedding[i] /= magnitude;
            }

            return embedding;
        }
    }
}
