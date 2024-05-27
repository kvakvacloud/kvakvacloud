using System.Security.Claims;
using UserDataService.Models.General.Responses;
using UserDataService.Models.Profile.Requests;

namespace UserDataService.Services.Profile;

public interface IProfileService
{
    public ApiResponse Get(GetProfileRequest model, ClaimsPrincipal userClaims);
    public ApiResponse Update(UpdateProfileRequest model , ClaimsPrincipal userClaims); 
}