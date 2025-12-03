using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.AI;
using HattieAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HattieAI.API.Hubs
{
    public class HattieHub : Hub
    {
        private readonly GeminiBroker _geminiBroker;
        private readonly HattieDbContext _dbContext;

        public HattieHub(GeminiBroker geminiBroker, HattieDbContext dbContext)
        {
            _geminiBroker = geminiBroker;
            _dbContext = dbContext;
        }

        public async Task SendMessage(string userMessage, Guid? chatSessionId)
        {
            var tenantIdString = Context.GetHttpContext()?.Request.Query["tenantId"].ToString();
            
            // 1. Fetch Tenant & Validate
            if (!Guid.TryParse(tenantIdString, out var tenantIdGuid))
            {
                 await Clients.Caller.SendAsync("ReceiveError", "Invalid Tenant ID format.");
                 return;
            }

            var tenant = await _dbContext.Tenants.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == tenantIdGuid);
            if (tenant == null)
            {
                 await Clients.Caller.SendAsync("ReceiveError", "Tenant not found.");
                 return;
            }

            var language = Context.GetHttpContext()?.Request.Query["language"].ToString() ?? "en";
            var isArabic = language.ToLower() == "ar";

            // 2. Construct Strict System Prompt
            var tenantName = tenant.Name ?? "the system";
            var systemInstruction = $@"You are a friendly, intelligent, and professional AI assistant for {tenantName}.

**Your Mission:**
Provide helpful, natural assistance while strictly adhering to the provided Context for all business information.

**CRITICAL RULES:**
1. **LANGUAGE**: {(isArabic ? "You MUST respond in ARABIC language only." : "You MUST respond in ENGLISH language only.")}
2. **VARY YOUR RESPONSES**: Never use the exact same phrase twice in a row.
3. **BE NATURAL**: Speak like a real human assistant. Avoid robotic or hardcoded-sounding phrases.
4. **CONTEXT IS KING**: For any question about {tenantName}, services, or products, you MUST derive your answer *only* from the provided Context.
5. **NO HALLUCINATIONS**: If the answer is not in the Context, do NOT make it up. Instead, politely apologize and suggest contacting the admin. {(isArabic ? "Say something like: 'عذراً، لا تتوفر لدي هذه المعلومة حالياً. يرجى التواصل مع الإدارة للمساعدة.'" : "Vary this apology too (e.g., 'I'm not sure about that one...', 'That info isn't available to me...', etc.).")}
6. **CLARIFY VAGUENESS**: If the user is unclear, ask for more details naturally.";
            var knowledgeBase = tenant.KnowledgeBaseText ?? "";
            Console.WriteLine($"[HattieHub] Tenant: {tenantName}, KB Length: {knowledgeBase.Length}");

            // 3. Handle Session
            ChatSession session;
            if (chatSessionId == null || chatSessionId == Guid.Empty)
            {
                session = new ChatSession
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantIdString,
                    Title = userMessage.Length > 20 ? userMessage.Substring(0, 20) + "..." : userMessage,
                    CreatedAt = DateTime.UtcNow
                };
                _dbContext.ChatSessions.Add(session);
                await _dbContext.SaveChangesAsync();
                
                await Clients.Caller.SendAsync("ReceiveSessionId", session.Id);
            }
            else
            {
                session = await _dbContext.ChatSessions.IgnoreQueryFilters().FirstOrDefaultAsync(s => s.Id == chatSessionId);
                if (session == null)
                {
                    await Clients.Caller.SendAsync("ReceiveError", "Session not found.");
                    return;
                }
            }

            // 4. Fetch History (Before saving current message to avoid duplication in prompt)
            var historyMessages = await _dbContext.ChatMessages
                .Where(m => m.ChatSessionId == session.Id)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
            
            var historyBuilder = new StringBuilder();
            foreach (var msg in historyMessages)
            {
                historyBuilder.AppendLine($"{msg.Role}: {msg.Content}");
            }
            var history = historyBuilder.ToString();

            // 5. Save User Message
            var userChatMsg = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ChatSessionId = session.Id,
                Role = "user",
                Content = userMessage,
                CreatedAt = DateTime.UtcNow,
                TenantId = tenantIdString
            };
            _dbContext.ChatMessages.Add(userChatMsg);
            await _dbContext.SaveChangesAsync();

            // 6. Call Gemini with Strict Persona
            await Clients.Caller.SendAsync("ReceiveMessageStart");
            
            var fullResponse = "";
            var responseStream = _geminiBroker.GenerateResponseStreamAsync(systemInstruction, knowledgeBase, history, userMessage);

            await foreach (var chunk in responseStream)
            {
                fullResponse += chunk;
                await Clients.Caller.SendAsync("ReceiveMessageChunk", chunk);
            }
            
            await Clients.Caller.SendAsync("ReceiveMessageEnd");

            // 7. Save AI Message
            var aiChatMsg = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ChatSessionId = session.Id,
                Role = "model",
                Content = fullResponse,
                CreatedAt = DateTime.UtcNow,
                TenantId = tenantIdString
            };
            _dbContext.ChatMessages.Add(aiChatMsg);
            await _dbContext.SaveChangesAsync();
        }
    }
}
