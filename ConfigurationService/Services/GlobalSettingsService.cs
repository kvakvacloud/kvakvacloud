using ConfigurationService.Model;
using ConfigurationService.Repository;
using ConfigurationService.Responses;

namespace ConfigurationService.Services;

public class GlobalSettingsService : IGlobalSettingsService
{
    private readonly IGlobalSettingRepository _gsRepo;

    public GlobalSettingsService(IGlobalSettingRepository gsRepo)
    {
        _gsRepo = gsRepo;
    }

    public ApiResponse Get(GetGlobalSettingModel model)
    {
        throw new NotImplementedException();
    }

    public ApiResponse Update(UpdateGlobalSettingModel model)
    {
        throw new NotImplementedException();
    }
}
