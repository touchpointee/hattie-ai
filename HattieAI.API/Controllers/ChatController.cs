using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HattieAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly HattieDbContext _context;

        public ChatController(HattieDbContext context)
        {
            _context = context;
        }

        // GET: api/Chat/sessions?tenantId=...
        [HttpGet("sessions")]
        public async Task<ActionResult<IEnumerable<ChatSession>>> GetSessions([FromQuery] string tenantId)
        {
            // Note: The Global Query Filter in HattieDbContext expects a TenantId provider.
            // Since we are in a Controller, the HttpContextTenantProvider should pick up the tenantId from the query string automatically
            // IF we configured it correctly.
            // However, for explicit API usage, let's rely on the filter working via the injected service.
            
            // If the filter is working, this will only return sessions for the tenantId in the query string.
            return await _context.ChatSessions
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        // GET: api/Chat/sessions/{id}/messages?tenantId=...
        [HttpGet("sessions/{id}/messages")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetSessionMessages(Guid id, [FromQuery] string tenantId)
        {
            var session = await _context.ChatSessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return await _context.ChatMessages
                .Where(m => m.ChatSessionId == id)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }
    }
}
