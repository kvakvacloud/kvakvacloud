namespace AuthService.Services;

public interface IAuthService
{
    bool Register(string email, string password);
    bool Login(string username, string password);
    bool RequestPasswordReset(string email);
    bool ResetPassword(string code, string newPassword);
}