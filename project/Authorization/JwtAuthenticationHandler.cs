using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Api.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Authorization {
    public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
        private readonly TokenManager TokenService;

        public JwtAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            TokenManager tokenService
        ): base(options, logger, encoder, clock) {
            TokenService = tokenService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            if (token != null) {
                try {
                    var decoded = TokenService.Decode(token);
                    
                    Context.Items["UserId"] = decoded.UserId;
                    Context.Items["AccessLevel"] = decoded.UserRole;
                    Context.Items["Login"] = decoded.Login;
                } catch (Exception ex) {
                    return Task.FromResult(AuthenticateResult.Fail(ex));
                }
                
            } else {
                return Task.FromResult(AuthenticateResult.Fail("Authorization token required"));
            }
            
            var claims = new Claim[] {
                //new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                //new Claim(ClaimTypes.Email, model.EmailAddress),
                //new Claim(ClaimTypes.Name, model.Name) 
            };

            var claimsIdentity = new ClaimsIdentity(claims, nameof(JwtAuthenticationHandler));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(
                new ClaimsPrincipal(claimsIdentity), 
                Scheme.Name
            )));
        }
    }
}