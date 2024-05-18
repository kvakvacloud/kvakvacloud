using AuthService.Model;
using AuthService.Responses;

namespace AuthService.Services;

public interface IAccountService
{
    public ApiResponse Register(string email);
    public ApiResponse Activate(AccountRegFormModel regform);
    public ApiResponse ValidateRegCode(AccountRegCodeModel model);
    public ApiResponse Login(string username, string password);
    public ApiResponse ChangePassword(AccountChangePasswordModel model);
    public ApiResponse RequestPasswordReset(string email);
    public ApiResponse ResetPassword(AccountPasswordResetModel model);
    public ApiResponse RefreshToken(AccountRefreshTokenModel model);
}