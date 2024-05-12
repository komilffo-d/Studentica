using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Studentica.Identity.Common.Helpers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Studentica.UI.Services
{
    public class OutsideJwtAuthenticationHandler : AuthenticationHandler<OutsideJwtAuthenticationSchemeOptions>
    {

        private readonly AuthService _authService;
        private readonly IMemoryCache _memoryCache;

        public OutsideJwtAuthenticationHandler(
            IOptionsMonitor<OutsideJwtAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            AuthService authService,
            IMemoryCache memoryCache) : base(options, logger, encoder)
        {
            _authService = authService;
            _memoryCache = memoryCache;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            string? token = await _authService.GetTokenAsync();
            TokenCache? tokenCache = null;
            bool IsValid = default, IsNonValid=default;

            if (token is null)
                return AuthenticateResult.NoResult();

            
            _memoryCache.TryGetValue(nameof(tokenCache), out tokenCache);

            IsNonValid = tokenCache is not null && !tokenCache.IsValidToken && tokenCache.Token == token;

            if(IsNonValid)
                return AuthenticateResult.Fail("Невалидный токен!");

            IsValid = (tokenCache is not null && tokenCache.IsValidToken && tokenCache.Token == token) || (await _authService.ValidateAsync(token));


            if (!IsValid)
            {
                _memoryCache.Set(nameof(tokenCache), new TokenCache(token, false),TimeSpan.FromMinutes(1));
                return AuthenticateResult.Fail("Невалидный токен!");
            }



            _memoryCache.Set(nameof(tokenCache), new TokenCache(token, true), TimeSpan.FromMinutes(1));

            var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "JWT");
            var user = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(user, Scheme.Name);
            return AuthenticateResult.Success(ticket);

        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 302;
            Response.Redirect("/authorize/login");
            return Task.CompletedTask;
            
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            var authResult = await HandleAuthenticateOnceSafeAsync();
            Response.StatusCode = 302;
            if (authResult.Succeeded)
                Response.Redirect("/");
            else
                Response.Redirect("/authorize/login");

        }
        private record class TokenCache(string Token,bool IsValidToken = false);
    }
}
