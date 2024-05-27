using UserDataService.Models.General.Responses;
using UserDataService.Models.Privacy.Requests;
using UserDataService.Repositories;

namespace UserDataService.Services.Privacy;

public class PrivacyService(IPrivacySettingsRepository privacyRepo) : IPrivacyService
{
    private readonly IPrivacySettingsRepository _privacyRepo = privacyRepo;
    public ApiResponse GetSettings(GetPrivacySettingsRequest model)
    {
        throw new NotImplementedException();
    }

    public ApiResponse UpdateSettings(UpdatePrivacySettingsRequest model)
    {
        throw new NotImplementedException();
    }
}
