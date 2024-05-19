using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConfigurationService.Utils;

public class JwtUtils : IJwtUtils {

    public JwtUtils()
    {
    }

    public JwtSecurityToken StringToJwtToken(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        
        return token;
    }

    public IEnumerable<Claim> JwtTokenClaims(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(jwt).Claims;
    }
}