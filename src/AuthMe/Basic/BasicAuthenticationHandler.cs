using AuthMe.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AuthMe.Basic
{
    internal class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IIdentityProvider _identityProvider;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> 
            options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock, 
            IIdentityProvider identityProvider) 
            : base(options, logger, encoder, clock)
        {
            _identityProvider = identityProvider;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey(Constants.Authorization))
            {
                return AuthenticateResult.NoResult();
            }

            List<Claim> claims;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[Constants.Authorization]);
                var credeitialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credeitialBytes).Split(new [] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                var res = await _identityProvider.Authenticate(username, password);

                if (res == null)
                {
                    return AuthenticateResult.Fail("user Not Found.");
                }
                claims = res.ToList();
            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail(e.Message); ;
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
    }
}
