using System.Security.Claims;
using UserDataService.Models.General.Responses;
using UserDataService.Models.Privacy.Requests;

namespace UserDataService.Services.Privacy;

public interface IPrivacyService
{
    public ApiResponse GetSettings(GetPrivacySettingsRequest model, ClaimsPrincipal userClaims);
    public ApiResponse UpdateSettings(UpdatePrivacySettingsRequest model, ClaimsPrincipal userClaims);
}