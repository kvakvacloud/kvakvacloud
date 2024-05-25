using System.Security.Claims;
using AuthService.Models.Requests;
using AuthService.Models.Responses;

namespace AuthService.Services;

public interface IAccountService
{
    public ApiResponse Register(string email);
    public ApiResponse FinishRegistration(AccountFinishRegistrationRequest regform);
    public ApiResponse ValidateRegCode(AccountValidateRegCodeRequest model);
    public ApiResponse Login(string username, string password);
    public ApiResponse ChangePassword(AccountChangePasswordRequest model, ClaimsPrincipal userClaims);
    public ApiResponse RequestPasswordReset(string email);
    public ApiResponse ResetPassword(AccountPasswordResetRequest model);
    public ApiResponse RefreshToken(ClaimsPrincipal userClaims);
}