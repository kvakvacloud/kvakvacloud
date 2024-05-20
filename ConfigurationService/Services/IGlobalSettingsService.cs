using ConfigurationService.Model;
using ConfigurationService.Responses;

namespace ConfigurationService.Services;

public interface IGlobalSettingsService
{
    public ApiResponse Get(GetGlobalSettingModel model);
    public ApiResponse Update(UpdateGlobalSettingModel model);
}