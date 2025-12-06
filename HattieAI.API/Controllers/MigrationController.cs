using HattieAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HattieAI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MigrationController : ControllerBase
    {
        private readonly HattieDbContext _context;
        private readonly ILogger<MigrationController> _logger;

        public MigrationController(HattieDbContext context, ILogger<MigrationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> RunMigrations()
        {
            try
            {
                _logger.LogInformation("Starting database migration...");
                
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                
                if (pendingMigrations.Any())
                {
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Database migration completed successfully.");
                    return Ok(new { Message = "Migrations applied successfully.", PendingMigrations = pendingMigrations });
                }
                else
                {
                    _logger.LogInformation("No pending migrations.");
                    return Ok(new { Message = "Database is already up to date." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating the database.");
                return StatusCode(500, new { Message = "An error occurred while migrating the database.", Error = ex.Message });
            }
        }
    }
}
