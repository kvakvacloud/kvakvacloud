using AuthService.Model;
using AuthService.Responses;

namespace AuthService.Services;

public interface IAccountService
{
    ApiResponse Register(string email);
    ApiResponse Activate(AccountRegFormModel regform);
    ApiResponse ValidateRegCode(string code);
    ApiResponse Login(string username, string password);
    ApiResponse RequestPasswordReset(string email);
    ApiResponse ResetPassword(string code, string newPassword);
}