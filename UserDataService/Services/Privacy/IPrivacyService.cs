using UserDataService.Models.General.Responses;
using UserDataService.Models.Privacy.Requests;

namespace UserDataService.Services.Privacy;

public interface IPrivacyService
{
    public ApiResponse GetSettings(GetPrivacySettingsRequest model);
    public ApiResponse UpdateSettings(UpdatePrivacySettingsRequest model);
}