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

            // Append or Replace? Let's append for now with a separator
            if (!string.IsNullOrEmpty(tenant.KnowledgeBaseText))
            {
                tenant.KnowledgeBaseText += "\n\n--- NEW DOCUMENT ---\n\n";
            }
            tenant.KnowledgeBaseText += extractedText;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Document processed and knowledge base updated.", TenantId = tenantId, TextLength = extractedText.Length });
        }
    }
}
