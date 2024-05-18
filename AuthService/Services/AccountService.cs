using AuthService.Database.Models;
using AuthService.Repository;
using AuthService.Responses;
using AuthService.Utils;

namespace AuthService.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepo;

    public AccountService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public ApiResponse Register(string email, string password)
    {
        User? user = _userRepo.GetUserByEmail(email);

        // Registration is disabled in server settings
        // if (false) return RegistrationResult.Forbidden;

        // User already exists, pretend to register
        if (user != null) return new ApiResponse{Code=501};

        return new ApiResponse{Code=501}; //Not implemented
    }

    public ApiResponse Login(string username, string password)
    {
        User? user = _userRepo.GetUserByUsername(username);

        if (user == null || BcryptUtils.VerifyPassword(password, user.Password ?? ""))
        {
            return new ApiResponse{Code=401};
        }

        return new ApiResponse {Code=501, Payload=new{token="123-123-123-123"}};
    }

    public ApiResponse RequestPasswordReset(string email)
    {
        return new ApiResponse{Code=501};; //todo
    }

    public ApiResponse ResetPassword(string code, string newPassword)
    {  
        return new ApiResponse{Code=501};; //todo
    }
}