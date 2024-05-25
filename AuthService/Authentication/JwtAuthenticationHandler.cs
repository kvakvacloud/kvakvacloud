using System.Security.Claims;
using System.Text.Encodings.Web;
using AuthService.Services.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace AuthService.Authentication;
public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IJwtService _jwtService;

    public JwtAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IJwtService jwtService)
        : base(options, logger, encoder, clock)
    {
        _jwtService = jwtService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            // Extract the token from the request headers or query parameters
            string? token = Request.Headers["Authorization"];
    
            if (token == null)
            {
                throw new NullReferenceException("No Authorization header present");
            }
    
            // Validate the token based on its type
            if (token.StartsWith("Bearer "))
            {
                // Access token verification
                string accessToken = token.Substring("Bearer ".Length);
                if (_jwtService.ValidateAccessToken(accessToken, out string? username))
                {
                    // Create a ClaimsIdentity with the user information
                    var claims = new[] { new Claim(ClaimTypes.Name, username!) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
    
                    return AuthenticateResult.Success(ticket);
                }
            }
            else if (token.StartsWith("Refresh "))
            {
                // Refresh token verification
                string refreshToken = token.Substring("Refresh ".Length);
                if (_jwtService.ValidateRefreshToken(refreshToken, out string? username))
                {
                    // Create a ClaimsIdentity with the user information
                    var claims = new[] { new Claim(ClaimTypes.Name, username!) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
    
                    return AuthenticateResult.Success(ticket);
                }
            }
            else if (token.StartsWith("Microservice "))
            {
                // Refresh token verification
                string refreshToken = token.Substring("Microservice ".Length);
                if (_jwtService.ValidateMicroserviceToken(refreshToken, out string? microservice))
                {
                    // Create a ClaimsIdentity with the user information
                    var claims = new[] { new Claim(ClaimTypes.Name, microservice!) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
    
                    return AuthenticateResult.Success(ticket);
                }
            }
    
            return AuthenticateResult.Fail("Invalid token");
        }
        catch (Exception)
        {
            return AuthenticateResult.Fail("Invalid token");
        }
    }
}
