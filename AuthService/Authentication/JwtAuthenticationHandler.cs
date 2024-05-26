using System.Security.Claims;
using System.Text.Encodings.Web;
using AuthService.Services.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace AuthService.Authentication;
public class JwtAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IJwtService jwtService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private readonly IJwtService _jwtService = jwtService;

    protected override async Task<AuthenticateResult> HandleAu
    thenticateAsync()
    {
        try
        {
            // Extract the token from the request headers or query parameters
            string? token = Request.Headers.Authorization;
    
            if (token == null)
            {
                throw new NullReferenceException("No Authorization header present");
            }
    
            // Validate the token based on its type
            if (token.StartsWith("Access "))
            {
                // Access token verification
                string accessToken = token["Access ".Length..];
                if (_jwtService.ValidateAccessToken(accessToken, out string? username))
                {
                    // Create a ClaimsIdentity with the user information
                    var claims = new[] 
                    {
                        new Claim(ClaimTypes.Name, username!),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType,"User"),
                        new Claim(ClaimTypes.AuthenticationMethod, "Access")
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
    
                    return AuthenticateResult.Success(ticket);
                }
            }
            else if (token.StartsWith("Refresh "))
            {
                // Refresh token verification
                string refreshToken = token["Refresh ".Length..];
                if (_jwtService.ValidateRefreshToken(refreshToken, out string? username))
                {
                    // Create a ClaimsIdentity with the user information
                    var claims = new[] 
                    {
                        new Claim(ClaimTypes.Name, username!),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType,"User"),
                        new Claim(ClaimTypes.AuthenticationMethod, "Refresh")
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
    
                    return AuthenticateResult.Success(ticket);
                }
            }
            else if (token.StartsWith("Microservice "))
            {
                // Refresh token verification
                string refreshToken = token["Microservice ".Length..];
                if (_jwtService.ValidateMicroserviceToken(refreshToken, out string? microservice))
                {
                    // Create a ClaimsIdentity with the user information
                    var claims = new[] 
                    {
                        new Claim(ClaimTypes.Name, microservice!),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType,"Admin"),
                        new Claim(ClaimTypes.AuthenticationMethod, "Microservice")
                    };
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
