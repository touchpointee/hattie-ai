using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;

namespace HattieAI.Portal.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private ClaimsPrincipal? _currentUser;

        public CustomAuthStateProvider(ProtectedLocalStorage localStorage, IHttpContextAccessor httpContextAccessor)
        {
            _localStorage = localStorage;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpUser = _httpContextAccessor.HttpContext?.User;
            if (httpUser?.Identity?.IsAuthenticated == true)
            {
                _currentUser = httpUser;
                return new AuthenticationState(httpUser);
            }

            if (_currentUser?.Identity?.IsAuthenticated == true)
                return new AuthenticationState(_currentUser);

            try
            {
                var userSessionResult = await _localStorage.GetAsync<UserSession>("UserSession");
                var userSession = userSessionResult.Success ? userSessionResult.Value : null;

                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                if (userSession.ExpiryTime < DateTime.Now)
                {
                    await _localStorage.DeleteAsync("UserSession");
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role),
                    new Claim("TenantId", userSession.TenantId)
                }, "CustomAuth"));

                _currentUser = claimsPrincipal;
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                userSession.ExpiryTime = DateTime.Now.AddDays(30);
                await _localStorage.SetAsync("UserSession", userSession);
                
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role),
                    new Claim("TenantId", userSession.TenantId)
                }, "CustomAuth"));
                _currentUser = claimsPrincipal;
            }
            else
            {
                await _localStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
                _currentUser = null;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }

    public class UserSession
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string TenantId { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
    }
}
