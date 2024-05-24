using AuthService.Database.Models;

namespace AuthService.Services.Jwt;

public interface IJwtService
{
    public string GrantJwtTokens(User user);
}