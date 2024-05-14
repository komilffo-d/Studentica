using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Studentica.Identity.Common.Data;
using Studentica.Identity.Common.Helpers;
using System.Security.Claims;

namespace Studentica.UI.Handlers
{
    public class TokenServerAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly AuthService _authService;
        private readonly IMemoryCache _memoryCache;
        private readonly ClaimsPrincipal _anonymus = new ClaimsPrincipal(new ClaimsIdentity() { });

        public TokenServerAuthenticationStateProvider(AuthService authService, IMemoryCache memoryCache)
        {

            _authService = authService;
            _memoryCache = memoryCache;

        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? token = await _authService.GetTokenAsync();
            TokenCache? tokenCache = null;
            bool IsValid = default, IsNonValid = default;

            if (token is null)
                return new AuthenticationState(_anonymus);


            _memoryCache.TryGetValue(nameof(tokenCache), out tokenCache);

            IsNonValid = tokenCache is not null && !tokenCache.IsValidToken && tokenCache.Token == token;

            if (IsNonValid)
                return new AuthenticationState(_anonymus);

            IsValid = (tokenCache is not null && tokenCache.IsValidToken && tokenCache.Token == token) || (await _authService.ValidateAsync(token));


            if (!IsValid)
            {
                _memoryCache.Set(nameof(tokenCache), new TokenCache(token, false), TimeSpan.FromMinutes(1));
                return new AuthenticationState(_anonymus);
            }



            _memoryCache.Set(nameof(tokenCache), new TokenCache(token, true), TimeSpan.FromMinutes(1));

            var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "JWT");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
        public Task NotifyAuthenticationStateAsync()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return Task.CompletedTask;
        }
    }
}
