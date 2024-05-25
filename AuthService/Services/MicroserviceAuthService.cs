using AuthService.Models.Requests;
using AuthService.Models.Responses;
using AuthService.Services.Jwt;
using AuthService.Utils;

namespace AuthService.Services;
public class MicroserviceAuthService(IJwtService jwtService) : IMicroserviceAuthService
{
    private readonly IJwtService _jwtService = jwtService;

    public ApiResponse ServiceToken(RequestServiceTokenRequest model)
    {
        if (model.Host == null || model.XRealIP == null)
        {
            return new ApiResponse{Code=400};
        }
        var newServiceToken = _jwtService.GenerateMicroserviceToken(model.Host);
        
        ServiceTokenResponse payload = new()
        {
            ServiceToken=newServiceToken
        };

        return new ApiResponse{Code=200, Payload=payload};
    }
}