using HattieAI.API.Hubs;
using HattieAI.Infrastructure.AI;
using HattieAI.Infrastructure.Documents;
using HattieAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;

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

// Tenant Provider
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, HttpContextTenantProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Enable CORS - Must be before UseHttpsRedirection and UseAuthorization
app.UseCors("AllowAll");

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
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
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
