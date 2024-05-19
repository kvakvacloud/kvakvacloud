using AuthService.Model;
using AuthService.Responses;
using AuthService.Utils;

namespace AuthService.Services;
public class MicroserviceAuthService : IMicroserviceAuthService
{
    private readonly IJwtUtils _jwtUtils;

    public MicroserviceAuthService(IJwtUtils jwtUtils)
    {
        _jwtUtils = jwtUtils;
    }

    public ApiResponse ServiceToken(RequestServiceTokenModel model)
    {
        if (model.Host == null || model.XRealIP == null)
        {
            return new ApiResponse{Code=400};
        }
        var newServiceToken = _jwtUtils.GenerateServiceJwtToken(model.Host, model.XRealIP);
        
        ServiceTokenResponse payload = new()
        {
            ServiceToken=newServiceToken
        };

        return new ApiResponse{Code=200, Payload=payload};
    }
}