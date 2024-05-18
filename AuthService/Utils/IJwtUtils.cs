using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using AuthService.Database.Models;
using System.Security.Claims;
namespace AuthService.Utils;

public interface IJwtUtils {
    public string GenerateJwtToken(User user);
    public bool ValidateJwtToken(string jwt);

    public IEnumerable<Claim> JwtTokenClaims(string jwt);
    public JwtSecurityToken StringToJwtToken(string jwt);
}