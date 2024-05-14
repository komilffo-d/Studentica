﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace Studentica.UI.Services
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

    }
}
