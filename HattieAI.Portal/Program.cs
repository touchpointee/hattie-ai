using HattieAI.Portal.Components;
using HattieAI.Infrastructure.Persistence;
using HattieAI.Infrastructure.Documents;
using HattieAI.Infrastructure.AI;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using HattieAI.Portal.Auth;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
        options.Cookie.Name = "Hattie.Portal.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ProtectedLocalStorage>();
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



builder.Services.AddDbContextFactory<HattieDbContext>(options =>
    options.UseNpgsql(connectionString));

// Infrastructure Services
builder.Services.AddHttpClient<GeminiBroker>();
builder.Services.AddHttpClient<HattieAI.Infrastructure.WhatsApp.WhatsAppMetaService>();
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

// Apply Migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HattieDbContext>();
    dbContext.Database.Migrate();
    await DefaultAdminAccount.EnsureAsync(dbContext);
}

// Configure the HTTP request pipeline.

var forwardedHeaderOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};
forwardedHeaderOptions.KnownNetworks.Clear();
forwardedHeaderOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedHeaderOptions);

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

app.MapPost("/auth/login", async (HttpContext httpContext, HattieDbContext context) =>
{
    var form = await httpContext.Request.ReadFormAsync();
    var login = form["UserNameOrEmail"].ToString().Trim().ToLowerInvariant();
    var password = form["Password"].ToString();

    if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        return Results.Redirect("/login?error=missing");

    var user = await context.AppUsers
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(u => u.Username.ToLower() == login || u.Email.ToLower() == login);

    if (user == null || !PasswordSecurity.VerifyPassword(password, user.PasswordHash))
        return Results.Redirect("/login?error=invalid");

    var claims = new List<Claim>
    {
        new(ClaimTypes.Name, user.Username),
        new(ClaimTypes.Role, user.Role),
        new("TenantId", user.TenantId)
    };
    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await httpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal,
        new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
            AllowRefresh = true
        });

    return Results.Redirect("/");
}).DisableAntiforgery();

app.MapGet("/auth/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public class AdminTenantProvider : ITenantProvider
{
    public string TenantId => string.Empty;
}
