using AuthService.Enums;

namespace AuthService.Services;

public interface IAccountService
{
    RegistrationResult Register(string email, string password);
    bool Login(string username, string password);
    bool RequestPasswordReset(string email);
    bool ResetPassword(string code, string newPassword);
}