using AuthService.Database.Models;
using AuthService.Models.Requests;
using AuthService.Repositories;
using AuthService.Models.Responses;
using AuthService.Utils;
using AuthService.Services.Jwt;
using System.Security.Claims;

namespace AuthService.Services;

public class AccountService(IUserRepository userRepo, IRegistrationCodeRepository regcodeRepo, IRevokedPasswordRepository revokedPasswordRepo, IJwtService jwtService) : IAccountService
{
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IRegistrationCodeRepository _regcodeRepo = regcodeRepo;
    private readonly IRevokedPasswordRepository _revokedPasswordRepo = revokedPasswordRepo;
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
        RegistrationCode? regcode = _regcodeRepo.GetRegistrationCodeByCode(new Guid(regform.Code));

        // Проверка валидности кода регистрации и его действительности
        if (regcode == null || regcode.Used || regcode.ValidUntil < DateTime.UtcNow)
        {
            return new ApiResponse{Code=401, Payload=new{Message="Specified registration code is invalid or expired."}};
        }

        // Проверка занятости имени пользователя
        User? existingUser = _userRepo.GetUserByUsername(regform.Username);
        if (existingUser != null)
        {
            return new ApiResponse{Code=407, Payload=new{Message=$"Username {regform.Username} is taken."}};
        }

        // Фиксация использования кода регистрации
        regcode.Used=true;
        regcode.WhenWasUsed=DateTime.UtcNow;
        bool regcodeUpdated = _regcodeRepo.UpdateRegistrationCode(regcode);

        //Создание нового пользователя
        User newUser = new() {
            Email=regcode.Email,
            Username=regform.Username!,
            Password=BcryptUtils.HashPassword(regform.Password),
            RegistrationDate=DateTime.UtcNow,
            PasswordChangeDate=DateTime.UtcNow
        };
  
        bool userCreated = _userRepo.CreateUser(newUser);

        if (!regcodeUpdated || !userCreated)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Failed to register user."}};
        }

        // Выдача токенов
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

        // Проверка существования пользователя и хеша пароля
        if (user == null || !BcryptUtils.VerifyPassword(password, user.Password ?? ""))
        {
            return new ApiResponse{Code=401, Payload=new {Message="Invalid username or password"}};
        }

        // Выдача токенов
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

        // Проверка совпадения паролей
        if (model.NewPassword == model.OldPassword)
        {
            return new ApiResponse{Code=400, Payload=new{Message="New and old passwords must not match"}};
        }

        // Проверка корректности старого пароля
        if (!BcryptUtils.VerifyPassword(model.OldPassword ?? "", user.Password ?? ""))
        {
            return new ApiResponse{Code=401, Payload=new{Message="Invalid old password"}};
        }

        // Проверка уникальности нового пароля среди старых
        bool wasPasswordUsed =_revokedPasswordRepo.GeRevokedPasswordsByUsername(username)!
        .Any(rp => BcryptUtils.VerifyPassword(model.NewPassword, rp.Password));
        if (wasPasswordUsed)
        {
            return new ApiResponse{Code=409, Payload=new{Message="This password has already been used before"}};
        }

        // Обновление пароля в базе данных
        user.Password = BcryptUtils.HashPassword(model.NewPassword ?? "");
        user.PasswordChangeDate = DateTime.UtcNow;
        user.ForcePasswordChange = false;
        bool isSuccess = _userRepo.UpdateUser(user);
        if (!isSuccess)
        {
            return new ApiResponse{Code=500, Payload=new{Message="Failed to update user data"}};
        }

        // Регистрация старого пароля в базе отозванных паролей
        _revokedPasswordRepo.CreateRevokedPassword(new RevokedPassword()
        {
            User=user,
            Password=model.OldPassword!
        });

        // Выдача токенов
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