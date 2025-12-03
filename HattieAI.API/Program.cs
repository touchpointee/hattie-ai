using HattieAI.API.Hubs;
using HattieAI.Infrastructure.AI;
using HattieAI.Infrastructure.Documents;
using HattieAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .SetIsOriginAllowed(_ => true) // Allow any origin
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Infrastructure Services
builder.Services.AddHttpClient<GeminiBroker>();
builder.Services.AddSingleton<DocumentBroker>();

// Database
builder.Services.AddDbContext<HattieDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tenant Provider
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, HttpContextTenantProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.MapHub<HattieHub>("/hattieHub");

app.Run();

// Simple Tenant Provider Implementation
public class HttpContextTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string TenantId 
    {
        get 
        {
            // Try to get from Query String (for SignalR) or Header (for API)
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return string.Empty;

            if (context.Request.Query.TryGetValue("tenantId", out var tenantId))
            {
                return tenantId.ToString();
            }
            
            // Fallback or other logic
            return string.Empty;
        }
    }
}
