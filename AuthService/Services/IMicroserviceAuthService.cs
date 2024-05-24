using AuthService.Models.Requests;
using AuthService.Models.Responses;

namespace AuthService.Services;
public interface IMicroserviceAuthService
{
    public ApiResponse ServiceToken(RequestServiceTokenRequest model);
}