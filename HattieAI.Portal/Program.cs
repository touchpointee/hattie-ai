using HattieAI.Portal.Components;
using HattieAI.Infrastructure.Persistence;
using HattieAI.Infrastructure.Documents;
using HattieAI.Infrastructure.AI;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using HattieAI.Portal.Auth;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

builder.Services.AddAuthenticationCore();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>(provider => (CustomAuthStateProvider)provider.GetRequiredService<AuthenticationStateProvider>());

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

// Infrastructure Services
builder.Services.AddHttpClient<GeminiBroker>();
builder.Services.AddSingleton<DocumentBroker>();

// Tenant Provider for Admin Portal
// For Admin Portal, we might not need tenant isolation or we are the "Super Admin".
// Let's implement a dummy provider that returns null or a specific admin tenant ID if needed.
// Or we can bypass the filter for admin operations if we had a way.
// For now, let's assume the Admin Portal sees everything or we just set a dummy tenant.
// Actually, the global query filter might hide data if we don't set a tenant ID.
// Let's make ITenantProvider return null to indicate "Global Admin" and update DbContext to handle null.
builder.Services.AddScoped<ITenantProvider, AdminTenantProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public class AdminTenantProvider : ITenantProvider
{
    public string TenantId => null; // Indicates no specific tenant filter, or we need to handle this in DbContext
}
