using AuthService.Database.Models;
using AuthService.Models.Requests;
using AuthService.Repositories;
using AuthService.Models.Responses;
using AuthService.Utils;
using AuthService.Services.Jwt;
using System.Security.Claims;

namespace AuthService.Services;

public class AccountService(IUserRepository userRepo, IRegistrationCodeRepository regcodeRepo, IJwtService jwtService) : IAccountService
{
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IRegistrationCodeRepository _regcodeRepo = regcodeRepo;
    private readonly IJwtService _jwtService = jwtService;

    public ApiResponse Register(string email)
    {
        User? existingUser = _userRepo.GetUserByEmail(email);

        // Registration is disabled in server settings
        // if (false) return RegistrationResult.Forbidden;

        // User already exists, pretend to register
        if (existingUser != null) return new ApiResponse{Code=200};

        RegistrationCode newRegistrationCode = new() {
            Code=Guid.NewGuid(),
            Email=email,
            ValidUntil=DateTime.UtcNow.AddDays(1),
            Used=false
        };

        _regcodeRepo.CreateRegistrationCode(newRegistrationCode);

        return new ApiResponse{Code=200};
    }

    public ApiResponse ValidateRegCode(AccountValidateRegCodeRequest model)
    {
        RegistrationCode? regcode = _regcodeRepo.GetRegistrationCodeByCode(new Guid(model.Code ?? ""));

        if (regcode == null || regcode.Used || regcode.ValidUntil < DateTime.UtcNow)
        {
            return new ApiResponse{Code=404, Payload=new{isValid=false}};
        }
        else
        {
            return new ApiResponse{Code=200, Payload=new{isValid=true}};
        }
    }

    public ApiResponse FinishRegistration(AccountFinishRegistrationRequest regform)
    {
        RegistrationCode? regcode = _regcodeRepo.GetRegistrationCodeByCode(new Guid(regform.Code ?? ""));

        if (regcode == null || regcode.Used || regcode.ValidUntil < DateTime.UtcNow)
        {
            return new ApiResponse{Code=401, Payload=new{Message="Specified registration code is invalid or expired."}};
        }

        User? existingUser = _userRepo.GetUserByUsername(regform.Username ?? "");
        if (existingUser != null)
        {
            return new ApiResponse{Code=407, Payload=new{Message=$"Username {regform.Username} is taken."}};
        }

        regcode.Used=true;
        regcode.WhenWasUsed=DateTime.UtcNow;

        User newUser = new() {
            Email=regcode.Email,
            Username=regform.Username!,
            Password=BcryptUtils.HashPassword(regform.Password),
            RegistrationDate=DateTime.UtcNow,
            PasswordChangeDate=DateTime.UtcNow
        };

        bool regcodeUpdated = _regcodeRepo.UpdateRegistrationCode(regcode);
        bool userCreated = _userRepo.CreateUser(newUser);

        if (!regcodeUpdated || !userCreated)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Failed to register user."}};
        }

        TokensResponse tokens = new() 
        {
            RefreshToken = _jwtService.GenerateAccessToken(newUser),
            AccessToken = _jwtService.GenerateRefreshToken(newUser)
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }
    public ApiResponse Login(string username, string password)
    {
        User? user = _userRepo.GetUserByUsername(username);

        if (user == null || !BcryptUtils.VerifyPassword(password, user.Password ?? ""))
        {
            return new ApiResponse{Code=401};
        }

        TokensResponse tokens = new() 
        {
            RefreshToken = _jwtService.GenerateRefreshToken(user),
            AccessToken = _jwtService.GenerateAccessToken(user)
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }

    public ApiResponse ChangePassword(AccountChangePasswordRequest model, ClaimsPrincipal userClaims)
    {
        var username = userClaims!.Identity!.Name!;
        User? user = _userRepo.GetUserByUsername(username);

        if (user == null)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Failed to retreive user"}};
        }

        if (!BcryptUtils.VerifyPassword(model.OldPassword ?? "", user.Password ?? ""))
        {
            return new ApiResponse{Code=401, Payload=new{Message="Invalid old password"}};
        }

        user.Password = BcryptUtils.HashPassword(model.NewPassword ?? "");
        user.PasswordChangeDate = DateTime.UtcNow;
        user.ForcePasswordChange = false;

        bool isSuccess = _userRepo.UpdateUser(user);

        if (!isSuccess)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Failed to update user data"}};
        }

        TokensResponse tokens = new() 
        {
            RefreshToken = _jwtService.GenerateRefreshToken(user),
            AccessToken = _jwtService.GenerateAccessToken(user)
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }

    public ApiResponse RequestPasswordReset(string email)
    {
        return new ApiResponse{Code=501}; //todo
    }

    public ApiResponse ResetPassword(AccountPasswordResetRequest model)
    {  
        return new ApiResponse{Code=501}; //todo
    }

    public ApiResponse RefreshToken(ClaimsPrincipal userClaims)
    {
        var username = userClaims!.Identity!.Name!;
        User? user = _userRepo.GetUserByUsername(username);
        if (user == null)
        {
            return new ApiResponse {Code=500, Payload=new{Message="Failed to retrieve user"}};
        }
        TokensResponse tokens = new() 
        {
            RefreshToken = _jwtService.GenerateRefreshToken(user),
            AccessToken = _jwtService.GenerateAccessToken(user)
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }

}