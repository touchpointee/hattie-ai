using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.Persistence;
using HattieAI.Infrastructure.Security;
using HattieAI.Infrastructure.WhatsApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HattieAI.API.Services
{
    public class WhatsAppSessionTimeoutService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WhatsAppSessionTimeoutService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30);

        public WhatsAppSessionTimeoutService(
            IServiceProvider serviceProvider,
            ILogger<WhatsAppSessionTimeoutService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("WhatsAppSessionTimeoutService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckActiveSessionsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking active WhatsApp sessions.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("WhatsAppSessionTimeoutService is stopping.");
        }

        private async Task CheckActiveSessionsAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HattieDbContext>();
            var metaService = scope.ServiceProvider.GetRequiredService<WhatsAppMetaService>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var encryptionKey = configuration["ENCRYPTION_KEY"] ?? Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? "";
            if (string.IsNullOrEmpty(encryptionKey))
            {
                _logger.LogWarning("ENCRYPTION_KEY is not configured. WhatsApp timeout service cannot decrypt access tokens.");
                return;
            }

            // Find all active, non-closed WhatsApp sessions
            var activeSessions = await dbContext.ChatSessions
                .IgnoreQueryFilters()
                .Where(s => s.Channel == "WhatsApp" && !s.IsClosed)
                .ToListAsync(stoppingToken);

            if (!activeSessions.Any()) return;

            var now = DateTime.UtcNow;
            var tenMinutesAgo = now.AddMinutes(-10);

            foreach (var session in activeSessions)
            {
                if (stoppingToken.IsCancellationRequested) break;

                // Find the latest message in this session
                var lastMessage = await dbContext.ChatMessages
                    .IgnoreQueryFilters()
                    .Where(m => m.ChatSessionId == session.Id)
                    .OrderByDescending(m => m.CreatedAt)
                    .FirstOrDefaultAsync(stoppingToken);

                if (lastMessage != null && lastMessage.CreatedAt < tenMinutesAgo)
                {
                    _logger.LogInformation("Session {SessionId} for WhatsApp user {UserId} is inactive for 10 minutes. Closing session.", session.Id, session.UserId);

                    // Fetch WhatsApp config for this tenant to send the closing thank you message
                    var config = await dbContext.WhatsAppConfigs
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(c => c.TenantId == session.TenantId, stoppingToken);

                    if (config != null && !string.IsNullOrEmpty(config.PhoneNumberId) && !string.IsNullOrEmpty(config.AccessToken))
                    {
                        try
                        {
                            var decryptedToken = EncryptionHelper.Decrypt(config.AccessToken, encryptionKey);
                            const string thankYouMessageText = "Thank you for chatting with us! This session is now closed due to inactivity. If you need further help, just reply to start a new chat.";

                            // Send via Meta API
                            await metaService.SendTextMessageAsync(config.PhoneNumberId, decryptedToken, session.UserId, thankYouMessageText);

                            // Save outbound message to DB
                            var thankYouMessage = new ChatMessage
                            {
                                Id = Guid.NewGuid(),
                                ChatSessionId = session.Id,
                                Role = "model",
                                Content = thankYouMessageText,
                                TenantId = session.TenantId,
                                CreatedAt = DateTime.UtcNow
                            };
                            dbContext.ChatMessages.Add(thankYouMessage);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to send inactivity thank you message to WhatsApp user {UserId}", session.UserId);
                        }
                    }

                    // Close the session
                    session.IsClosed = true;
                }
            }

            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}
