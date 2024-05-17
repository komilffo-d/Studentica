using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace Studentica.UI.Handlers
{
    public class OutsideJwtAuthenticationHandler : AuthenticationHandler<OutsideJwtAuthenticationSchemeOptions>
    {

        public OutsideJwtAuthenticationHandler(
            IOptionsMonitor<OutsideJwtAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder) : base(options, logger, encoder)
        {

        }


        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new GenericIdentity("GenericIdentity")), "OutsideJwtAuthenticationScheme")));

        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 302;
            Response.Redirect("/");
            return Task.CompletedTask;
        }
    }
}
