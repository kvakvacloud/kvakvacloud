using AuthService.Model;
using AuthService.Responses;

namespace AuthService.Services;
public interface IMicroserviceAuthService
{
    public ApiResponse ServiceToken(RequestServiceTokenModel model);
}