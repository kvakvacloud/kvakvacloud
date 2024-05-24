using AuthService.Model;
using AuthService.Responses;

namespace AuthService.Services;

public interface IAccountService
{
    public ApiResponse Register(string email);
    public ApiResponse FinishRegistration(AccountFinishRegistrationRequest regform);
    public ApiResponse ValidateRegCode(AccountValidateRegCodeRequest model);
    public ApiResponse Login(string username, string password);
    public ApiResponse ChangePassword(AccountChangePasswordRequest model);
    public ApiResponse RequestPasswordReset(string email);
    public ApiResponse ResetPassword(AccountPasswordResetRequest model);
    public ApiResponse RefreshToken(AccountRefreshTokenModel model);
}