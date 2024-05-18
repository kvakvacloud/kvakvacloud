using AuthService.Database.Models;
using AuthService.Model;
using AuthService.Repository;
using AuthService.Responses;
using AuthService.Utils;

namespace AuthService.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepo;
    private readonly IRegistrationCodeRepository _regcodeRepo;
    private readonly IJwtUtils _jwtUtils;

    public AccountService(IUserRepository userRepo, IRegistrationCodeRepository regcodeRepo, IJwtUtils jwtUtils)
    {
        _userRepo = userRepo;
        _regcodeRepo = regcodeRepo;
        _jwtUtils = jwtUtils;
    }

    public ApiResponse Register(string email)
    {
        User? existingUser = _userRepo.GetUserByEmail(email);

        // Registration is disabled in server settings
        // if (false) return RegistrationResult.Forbidden;

        // User already exists, pretend to register
        if (existingUser != null) return new ApiResponse{Code=200};

        RegistrationCode newRegistrationCode = new() {
            Code=new Guid(),
            Email=email,
            ValidUntil=DateTime.UtcNow.AddDays(1),
            Used=false
        };

        _regcodeRepo.CreateRegistrationCode(newRegistrationCode);

        return new ApiResponse{Code=200};
    }

    public ApiResponse ValidateRegCode(string code)
    {
        RegistrationCode? regcode = _regcodeRepo.GetRegistrationCodeByCode(new Guid(code));

        if (regcode == null || regcode.Used || regcode.ValidUntil < DateTime.UtcNow)
        {
            return new ApiResponse{Code=200, Payload=new{isValid=false}};
        }
        else
        {
            return new ApiResponse{Code=200, Payload=new{isValid=true}};
        }
    }

    public ApiResponse Activate(AccountRegFormModel regform)
    {
        RegistrationCode? regcode = _regcodeRepo.GetRegistrationCodeByCode(new Guid(regform.Code));

        if (regcode == null || regcode.Used || regcode.ValidUntil < DateTime.UtcNow)
        {
            return new ApiResponse{Code=401};
        }

        User? existingUser = _userRepo.GetUserByUsername(regform.Username);
        if (existingUser != null)
        {
            return new ApiResponse{Code=407, Payload=new{Message=$"Username {regform.Username} is taken."}};
        }

        regcode.Used=true;
        regcode.WhenWasUsed=DateTime.UtcNow;

        User? newUser = new() {
            Email=regcode.Email,
            Username=regform.Username,
            Password=regform.Password,
            RegistrationDate=DateTime.UtcNow,
            PasswordChangeDate=DateTime.UtcNow
        };

        bool regcodeUpdated = _regcodeRepo.UpdateRegistrationCode(regcode);
        bool userCreated = _userRepo.CreateUser(newUser);

        if (!regcodeUpdated || !userCreated)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Failed to register user."}};
        }

        return new ApiResponse{Code=200};
    }
    public ApiResponse Login(string username, string password)
    {
        User? user = _userRepo.GetUserByUsername(username);

        if (user == null || BcryptUtils.VerifyPassword(password, user.Password ?? ""))
        {
            return new ApiResponse{Code=401};
        }

        string token = _jwtUtils.GenerateJwtToken(user);

        return new ApiResponse {Code=200, Payload=new{Token=token}};
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