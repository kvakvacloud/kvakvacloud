using UserDataService.Models.General.Responses;
using UserDataService.Models.Profile.Requests;

namespace UserDataService.Services.Profile;

public interface IProfileService
{
    public ApiResponse Get(GetProfileRequest model);
    public ApiResponse Update(UpdateProfileRequest model); 
}