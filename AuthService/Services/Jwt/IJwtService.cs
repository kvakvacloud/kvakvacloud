using AuthService.Database.Models;

namespace AuthService.Services.Jwt;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    string GenerateMicroserviceToken(string microserviceId);
    bool ValidateAccessToken(string token);
    bool ValidateRefreshToken(string token);
    bool ValidateMicroserviceToken(string token, out string? microservice);
}
