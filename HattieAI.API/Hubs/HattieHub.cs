using HattieAI.Domain.Entities;
using HattieAI.Infrastructure.AI;
using HattieAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HattieAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HattieAI.API.Hubs
{
    public class HattieHub : Hub
    {
        private readonly GroqBroker _groqBroker;
        private readonly HattieDbContext _dbContext;

        public HattieHub(GroqBroker groqBroker, HattieDbContext dbContext)
        {
            _groqBroker = groqBroker;
            _dbContext = dbContext;
        }

        public async Task SendMessage(string userMessage, Guid? chatSessionId)
        {
            try
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

                var languageCode = Context.GetHttpContext()?.Request.Query["language"].ToString() ?? "en";
                var languageName = "English";
            
                var languageEntity = await _dbContext.Languages.FirstOrDefaultAsync(l => l.Code == languageCode);
                if (languageEntity != null)
                {
                    languageName = languageEntity.Name;
                }

                // 2. Retrieval (Vector Search)
                var knowledgeBase = "";
            
                // Generate embedding for user query
                var queryEmbedding = await GenerateSimpleEmbeddingAsync(userMessage);
            
                if (queryEmbedding.Length > 0)
                {
                    // Fetch all chunks for this tenant (In-Memory Search for now)
                    var chunks = await _dbContext.KnowledgeChunks
                                        .Where(k => k.TenantId == tenantIdString)
                                        .ToListAsync();
                
                    if (chunks.Any())
                    {
                        var scoredChunks = chunks
                            .Select(c => new 
                            { 
                                Chunk = c, 
                                Score = CosineSimilarity(c.Embedding, queryEmbedding) 
                            })
                            .OrderByDescending(x => x.Score)
                            .Take(3)
                            .ToList();
                    
                        knowledgeBase = string.Join("\n\n---\n\n", scoredChunks.Select(s => s.Chunk.Content));
                        Console.WriteLine($"[HattieHub] Vector Search: Found {scoredChunks.Count} relevant chunks.");
                    }
                    else
                    {
                        // Fallback to legacy full text if no chunks exist
                        knowledgeBase = tenant.KnowledgeBaseText ?? "";
                        Console.WriteLine($"[HattieHub] Vector Search: No chunks found. Using legacy text.");
                    }
                }
                else
                {
                     // Fallback if embedding fails
                     knowledgeBase = tenant.KnowledgeBaseText ?? "";
                }

                // 3. Construct Strict System Prompt
                var tenantName = tenant.Name ?? "the system";
                var systemInstruction = $@"You are a friendly, intelligent, and professional AI assistant for {tenantName}.

**Your Mission:**
Provide helpful, natural assistance while strictly adhering to the provided Context for all business information.

**CRITICAL RULES:**
1. **LANGUAGE**: You MUST respond in {languageName?.ToUpper() ?? "ENGLISH"} language only.
2. **VARY YOUR RESPONSES**: Never use the exact same phrase twice in a row.
3. **BE NATURAL**: Speak like a real human assistant. Avoid robotic or hardcoded-sounding phrases.
4. **CONTEXT IS KING**: For any question about {tenantName}, services, or products, you MUST derive your answer *only* from the provided Context.
5. **NO HALLUCINATIONS**: If the answer is not in the Context, do NOT make it up. Instead, politely apologize and suggest contacting the admin. (e.g., 'I'm not sure about that one...', 'That info isn't available to me...', etc. - translate this to {languageName} if needed).
6. **NO FILLER**: Do NOT use phrases like 'I'd be happy to help', 'Great question', or 'Hello there'. Start the answer immediately.
7. **SHORT ANSWERS**: Detailed essays are BANNED. Max 3 sentences or bullet points.
8. **CLARIFY NATURALLY**: If the user's message is random or completely unclear, ask a specific question to understand their needs. Do NOT say 'That is a vague query'. Be helpful, polite, and human-like.";
            
                Console.WriteLine($"[HattieHub] Tenant: {tenantName}, Language: {languageName}, KB Length: {knowledgeBase.Length}");

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
                // Limit to last 5 messages to reduce token costs
                var historyMessages = await _dbContext.ChatMessages
                    .Where(m => m.ChatSessionId == session.Id)
                    .OrderByDescending(m => m.CreatedAt)
                    .Take(5)
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

                // 6. Call Groq with Strict Persona
                await Clients.Caller.SendAsync("ReceiveMessageStart");
            
                var fullResponse = "";
                var responseStream = _groqBroker.GenerateResponseStreamAsync(systemInstruction, knowledgeBase, history, userMessage);

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
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] SendMessage Failed: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                await Clients.Caller.SendAsync("ReceiveError", $"Server Error: {ex.Message}");
                throw; // Rethrow to let SignalR handle it too if needed, but we already sent info to client
            }
        }


        private static float CosineSimilarity(float[] vectorA, float[] vectorB)
        {
            if (vectorA == null || vectorB == null || vectorA.Length != vectorB.Length)
                return 0f;

            float dotProduct = 0f;
            float magnitudeA = 0f;
            float magnitudeB = 0f;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                magnitudeA += vectorA[i] * vectorA[i];
                magnitudeB += vectorB[i] * vectorB[i];
            }

            if (magnitudeA == 0 || magnitudeB == 0) return 0f;

            return dotProduct / ((float)Math.Sqrt(magnitudeA) * (float)Math.Sqrt(magnitudeB));
        }

        /// <summary>
        /// Simple hash-based embedding for vector search (Groq doesn't have embeddings API).
        /// For production, consider using a dedicated embedding service.
        /// </summary>
        private Task<float[]> GenerateSimpleEmbeddingAsync(string text)
        {
            // Simple bag-of-words style embedding using hash
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

            return Task.FromResult(embedding);
        }
    }
}
