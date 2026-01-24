using HattieAI.API.Hubs;
using HattieAI.Infrastructure.AI;
using HattieAI.Infrastructure.Documents;
using HattieAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using DotNetEnv;

// Load .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

// Tenant Provider
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, HttpContextTenantProvider>();

// CORS configuration - Custom Provider
builder.Services.AddSingleton<Microsoft.AspNetCore.Cors.Infrastructure.ICorsPolicyProvider, HattieAI.API.Services.TenantCorsPolicyProvider>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DynamicTenantPolicy", builder => builder
        .SetIsOriginAllowed(_ => true) // Fallback/Initial setup, strictly overwritten by provider usually
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// Infrastructure Services
builder.Services.AddHttpClient<GroqBroker>();
builder.Services.AddSingleton<DocumentBroker>();

// Database
// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgres://"))
{
    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':');
    var builderStr = new Npgsql.NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port,
        Username = userInfo[0],
        Password = userInfo[1],
        Database = uri.AbsolutePath.TrimStart('/')
    }.ToString();
    connectionString = builderStr;
}

builder.Services.AddDbContext<HattieDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Optimize: Apply Migrations at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var dbContext = services.GetRequiredService<HattieDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Enable CORS - Must be before UseHttpsRedirection and UseAuthorization
app.UseCors("DynamicTenantPolicy");

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    var dbContext = context.RequestServices.GetService<HattieDbContext>();
    var tenantProvider = context.RequestServices.GetService<ITenantProvider>();
    if (dbContext != null && tenantProvider != null)
    {
        dbContext.CurrentTenantId = tenantProvider.TenantId;
    }
    await next();
});

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Allow loading static assets (like the chatbot script) from any origin
        ctx.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        ctx.Context.Response.Headers["Access-Control-Allow-Headers"] = "Origin, X-Requested-With, Content-Type, Accept";
    }
});

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
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return string.Empty;

            // 1. Try Query String (SignalR, etc.)
            if (context.Request.Query.TryGetValue("tenantId", out var tenantIdQuery))
            {
                return tenantIdQuery.ToString();
            }

            // 2. Try Headers
            if (context.Request.Headers.TryGetValue("X-Tenant-ID", out var tenantIdHeader))
            {
                return tenantIdHeader.ToString();
            }

            // 3. Try Route Pattern (Api/Tenants/{id})
            // Since CORS runs before routing might be fully finalized or if we just want to be robust:
            var path = context.Request.Path.Value;
            if (!string.IsNullOrEmpty(path))
            {
                // Regex for /api/Tenants/{guid}
                // Case insensitive check
                var match = System.Text.RegularExpressions.Regex.Match(path, @"/api/Tenants/([0-9a-fA-F-]{36})", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }

            // 4. Fallback: Check standard routing if available (might be null if CORS middleware is early)
            if (context.Request.RouteValues.TryGetValue("id", out var idRoute) && idRoute != null)
            {
                 return idRoute.ToString() ?? string.Empty;
            }
            
            return string.Empty;
        }
    }
}
