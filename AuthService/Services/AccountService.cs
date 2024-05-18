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
            Code=Guid.NewGuid(),
            Email=email,
            ValidUntil=DateTime.UtcNow.AddDays(1),
            Used=false
        };

        _regcodeRepo.CreateRegistrationCode(newRegistrationCode);

        return new ApiResponse{Code=200};
    }

    public ApiResponse ValidateRegCode(AccountRegCodeModel model)
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

    public ApiResponse Activate(AccountRegFormModel regform)
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

        TokensResponse tokens = new() 
        {
            RefreshToken = _jwtUtils.GenerateJwtToken(newUser, "refresh"),
            AccessToken = _jwtUtils.GenerateJwtToken(newUser, "access")
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }
    public ApiResponse Login(string username, string password)
    {
        User? user = _userRepo.GetUserByUsername(username);

        if (user == null || BcryptUtils.VerifyPassword(password, user.Password ?? ""))
        {
            return new ApiResponse{Code=401};
        }

        string refreshToken = _jwtUtils.GenerateJwtToken(user, "refresh");

        TokensResponse tokens = new() 
        {
            RefreshToken = _jwtUtils.GenerateJwtToken(user, "refresh"),
            AccessToken = _jwtUtils.GenerateJwtToken(user, "access")
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }

    public ApiResponse ChangePassword(AccountChangePasswordModel model)
    {
        if (!_jwtUtils.ValidateJwtToken(model.AccessToken ?? ""))
        {
            return new ApiResponse{Code=401};
        }
        User? user = _userRepo.GetUserByUsername(_jwtUtils.JwtTokenClaims(model.AccessToken ?? "").First(c => c.Type == "Username").Value);

        if (user == null)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Unexpected token problem"}};
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
            RefreshToken = _jwtUtils.GenerateJwtToken(user, "refresh"),
            AccessToken = _jwtUtils.GenerateJwtToken(user, "access")
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }

    public ApiResponse RequestPasswordReset(string email)
    {
        return new ApiResponse{Code=501}; //todo
    }

    public ApiResponse ResetPassword(AccountPasswordResetModel model)
    {  
        return new ApiResponse{Code=501}; //todo
    }

    public ApiResponse RefreshToken(AccountRefreshTokenModel model)
    {
        if (!_jwtUtils.ValidateJwtToken(model.RefreshToken ?? ""))
        {
            return new ApiResponse{Code=401, Payload=new{Message="Token not validated"}};
        }
        
        var token = _jwtUtils.StringToJwtToken(model.RefreshToken ?? "");

        if (token.Claims.First(c => c.Type == "Type").Value != "refresh")
        {
            return new ApiResponse{Code=401, Payload=new{Message="Wrong token type"}};
        }

        User? user = _userRepo.GetUserByUsername(token.Claims.First(c => c.Type == "Username").Value);

        if (user == null)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Unexpected token problem"}};
        }

        TokensResponse tokens = new() 
        {
            RefreshToken = _jwtUtils.GenerateJwtToken(user, "refresh"),
            AccessToken = _jwtUtils.GenerateJwtToken(user, "access")
        };

        return new ApiResponse {Code=200, Payload=tokens};
    }

}