using AuthService.Database.Models;
using AuthService.Repository;

namespace AuthService.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;

    public AuthService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public bool Register(string email, string password)
    {
        User? user = _userRepo.GetUserByEmail(email);

        //User already exists, pretend to register
        if (user != null) return true;

        
        return false; //todo
    }

    public bool Login(string username, string password)
    {
        return false; //todo
    }

    public bool RequestPasswordReset(string email)
    {
        return false; //todo
    }

    public bool ResetPassword(string code, string newPassword)
    {  
        return false; //todo
    }
}