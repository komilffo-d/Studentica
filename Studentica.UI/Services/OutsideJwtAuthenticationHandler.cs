using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Studentica.Identity.Common.Helpers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Studentica.UI.Services
{
    public class OutsideJwtAuthenticationHandler : AuthenticationHandler<OutsideJwtAuthenticationSchemeOptions>
    {

        private readonly AuthService _authService;

        public OutsideJwtAuthenticationHandler(
            IOptionsMonitor<OutsideJwtAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            TimeProvider clock,
            AuthService authService) : base(options, logger, encoder)
        {
            _authService = authService;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            string? token = await _authService.GetTokenAsync();

            if (token is not null)
            {
                var IsValidToken = await _authService.ValidateAsync(token);
                if (!IsValidToken)
                    return AuthenticateResult.Fail("Невалидный токен!");

                var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
                var user = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(user, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {

                return AuthenticateResult.NoResult();
            }
                

        }
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {

            var authResult = await HandleAuthenticateOnceSafeAsync();
            if (!authResult.Succeeded)
            {
                Response.StatusCode = 302;
                Response.Redirect("/authorize/login");
            }
        }
    }
}
