using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
namespace ConfigurationService.Utils;

public interface IJwtUtils {
    public IEnumerable<Claim> JwtTokenClaims(string jwt);
    public JwtSecurityToken StringToJwtToken(string jwt);
}