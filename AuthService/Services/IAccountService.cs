using AuthService.Model;
using AuthService.Responses;

namespace AuthService.Services;

public interface IAccountService
{
    ApiResponse Register(string email);
    ApiResponse Activate(AccountRegFormModel regform);
    ApiResponse ValidateRegCode(AccountRegCodeModel model);
    ApiResponse Login(string username, string password);
    ApiResponse ChangePassword(AccountChangePasswordModel model);
    ApiResponse RequestPasswordReset(string email);
    ApiResponse ResetPassword(AccountPasswordResetModel model);
}