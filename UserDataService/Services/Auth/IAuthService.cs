using System.Security.Claims;

namespace UserDataService.Services.Auth;

public interface IJwtService
{
    public List<Claim> VerifyAccessToken(string token);
    public bool VerifyMicroserviceToken(string token);
    public string GetMicroserviceToken(); 
}