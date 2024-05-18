using AuthService.Responses;

namespace AuthService.Services;

public interface IAccountService
{
    ApiResponse Register(string email, string password);
    ApiResponse Login(string username, string password);
    ApiResponse RequestPasswordReset(string email);
    ApiResponse ResetPassword(string code, string newPassword);
}