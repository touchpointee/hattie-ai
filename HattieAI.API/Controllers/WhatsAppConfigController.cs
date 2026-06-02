using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Persistence;
using HattieAI.Infrastructure.Security;
using HattieAI.Infrastructure.WhatsApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HattieAI.API.Controllers
{
    [ApiController]
    [Route("api/whatsapp/config")]
    public class WhatsAppConfigController : ControllerBase
    {
        private readonly HattieDbContext _context;
        private readonly WhatsAppMetaService _metaService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WhatsAppConfigController> _logger;

        public WhatsAppConfigController(
            HattieDbContext context,
            WhatsAppMetaService metaService,
            IConfiguration configuration,
            ILogger<WhatsAppConfigController> _logger)
        {
            _context = context;
            _metaService = metaService;
            _configuration = configuration;
            this._logger = _logger;
        }

        // GET /api/whatsapp/config?tenantId=...
        [HttpGet]
        public async Task<IActionResult> GetConfig([FromQuery] string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId)) return BadRequest("TenantId is required");

            var config = await _context.WhatsAppConfigs
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.TenantId == tenantId);

            if (config == null) return NotFound("No configuration found");

            var encryptionKey = _configuration["ENCRYPTION_KEY"] ?? Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? "";
            var decryptedVerifyToken = "";

            if (!string.IsNullOrEmpty(config.VerifyToken) && !string.IsNullOrEmpty(encryptionKey))
            {
                try
                {
                    decryptedVerifyToken = EncryptionHelper.Decrypt(config.VerifyToken, encryptionKey);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to decrypt verify token: {Message}", ex.Message);
                }
            }

            return Ok(new
            {
                config.Id,
                config.TenantId,
                config.PhoneNumberId,
                config.WabaId,
                config.Status,
                config.ConnectedAt,
                VerifyToken = decryptedVerifyToken,
                AccessToken = "••••••••••••••••" // Masked for security
            });
        }

        // POST /api/whatsapp/config
        [HttpPost]
        public async Task<IActionResult> SaveConfig([FromBody] WhatsAppConfigSaveDto dto)
        {
            if (string.IsNullOrEmpty(dto.TenantId) || string.IsNullOrEmpty(dto.PhoneNumberId))
            {
                return BadRequest("TenantId and PhoneNumberId are required");
            }

            var encryptionKey = _configuration["ENCRYPTION_KEY"] ?? Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? "";
            if (string.IsNullOrEmpty(encryptionKey))
            {
                return StatusCode(500, "Encryption key is not configured on server.");
            }

            // If we are saving, we need to test connection with Meta API first
            var testToken = dto.AccessToken;
            var config = await _context.WhatsAppConfigs
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.TenantId == dto.TenantId);

            if (testToken == "••••••••••••••••" || string.IsNullOrEmpty(testToken))
            {
                if (config != null)
                {
                    // Keep existing token
                    try
                    {
                        testToken = EncryptionHelper.Decrypt(config.AccessToken, encryptionKey);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Failed to decrypt existing token: {ex.Message}");
                    }
                }
                else
                {
                    return BadRequest("Access token is required for initial configuration");
                }
            }

            // Verify with Meta
            MetaPhoneInfo phoneInfo;
            try
            {
                phoneInfo = await _metaService.VerifyPhoneNumberAsync(dto.PhoneNumberId, testToken);
            }
            catch (Exception ex)
            {
                return BadRequest($"Meta API verification failed: {ex.Message}");
            }

            // Encrypt tokens
            var encryptedAccessToken = EncryptionHelper.Encrypt(testToken, encryptionKey);
            var encryptedVerifyToken = string.IsNullOrEmpty(dto.VerifyToken) ? "" : EncryptionHelper.Encrypt(dto.VerifyToken, encryptionKey);

            if (config == null)
            {
                config = new WhatsAppConfig
                {
                    Id = Guid.NewGuid(),
                    TenantId = dto.TenantId,
                    PhoneNumberId = dto.PhoneNumberId,
                    WabaId = dto.WabaId,
                    AccessToken = encryptedAccessToken,
                    VerifyToken = encryptedVerifyToken,
                    Status = "connected",
                    ConnectedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                _context.WhatsAppConfigs.Add(config);
            }
            else
            {
                config.PhoneNumberId = dto.PhoneNumberId;
                config.WabaId = dto.WabaId;
                config.AccessToken = encryptedAccessToken;
                config.VerifyToken = encryptedVerifyToken;
                config.Status = "connected";
                config.ConnectedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = true, displayPhone = phoneInfo.DisplayPhoneNumber, verifiedName = phoneInfo.VerifiedName });
        }

        // DELETE /api/whatsapp/config?tenantId=...
        [HttpDelete]
        public async Task<IActionResult> DeleteConfig([FromQuery] string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId)) return BadRequest("TenantId is required");

            var config = await _context.WhatsAppConfigs
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.TenantId == tenantId);

            if (config != null)
            {
                _context.WhatsAppConfigs.Remove(config);
                await _context.SaveChangesAsync();
            }

            return Ok(new { success = true });
        }

        // GET /api/whatsapp/config/rules?tenantId=...
        [HttpGet("rules")]
        public async Task<ActionResult<IEnumerable<WhatsAppAutomationRule>>> GetRules([FromQuery] string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId)) return BadRequest("TenantId is required");

            return await _context.WhatsAppAutomationRules
                .IgnoreQueryFilters()
                .Where(r => r.TenantId == tenantId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        // POST /api/whatsapp/config/rules
        [HttpPost("rules")]
        public async Task<IActionResult> SaveRule([FromBody] WhatsAppAutomationRule rule)
        {
            if (string.IsNullOrEmpty(rule.TenantId) || string.IsNullOrEmpty(rule.TriggerKeyword) || string.IsNullOrEmpty(rule.ReplyText))
            {
                return BadRequest("TenantId, TriggerKeyword, and ReplyText are required");
            }

            if (rule.Id == Guid.Empty)
            {
                rule.Id = Guid.NewGuid();
                rule.CreatedAt = DateTime.UtcNow;
                _context.WhatsAppAutomationRules.Add(rule);
            }
            else
            {
                var existing = await _context.WhatsAppAutomationRules
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(r => r.Id == rule.Id);

                if (existing == null) return NotFound("Rule not found");

                existing.TriggerKeyword = rule.TriggerKeyword;
                existing.MatchType = rule.MatchType;
                existing.ReplyText = rule.ReplyText;
                existing.IsActive = rule.IsActive;
            }

            await _context.SaveChangesAsync();
            return Ok(rule);
        }

        // DELETE /api/whatsapp/config/rules/{id}
        [HttpDelete("rules/{id}")]
        public async Task<IActionResult> DeleteRule(Guid id)
        {
            var rule = await _context.WhatsAppAutomationRules
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rule != null)
            {
                _context.WhatsAppAutomationRules.Remove(rule);
                await _context.SaveChangesAsync();
            }

            return Ok(new { success = true });
        }
    }

    public class WhatsAppConfigSaveDto
    {
        public string TenantId { get; set; } = string.Empty;
        public string PhoneNumberId { get; set; } = string.Empty;
        public string? WabaId { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string VerifyToken { get; set; } = string.Empty;
    }
}
