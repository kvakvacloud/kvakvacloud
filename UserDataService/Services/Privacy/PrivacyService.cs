using System.Security.Claims;
using UserDataService.Models.General.Responses;
using UserDataService.Models.Privacy.Requests;
using UserDataService.Models.Privacy.Responses;
using UserDataService.Repositories;

namespace UserDataService.Services.Privacy;

public class PrivacyService(IPrivacySettingsRepository privacyRepo) : IPrivacyService
{
    private readonly IPrivacySettingsRepository _privacyRepo = privacyRepo;
    public ApiResponse GetSettings(GetPrivacySettingsRequest model, ClaimsPrincipal userClaims)
    {
        if (model.ForUsername != null)
        {
            if (!userClaims.IsInRole("Admin"))
            {
                return new ApiResponse{Code=403};
            }
            var foreignSettings = _privacyRepo.GetUserPrivacySettingsByUsername(model.ForUsername);
            return new ApiResponse{Code=200, Payload=foreignSettings};
        }

        var settings = _privacyRepo.GetUserPrivacySettingsByUsername(userClaims!.Identity!.Name!);
        if (settings == null)
        {
            return new ApiResponse{Code=404, Payload=new {Message="Settings not found."}};
        }

        var response = new PrivacySettingsResponse
        {
            AllowUnauthorizedView = settings.AllowUnauthorizedView,
            HideInSearch = settings.HideInSearch,
            HideProfile = settings.HideProfile
        };
        return new ApiResponse{Code=200, Payload=response};
    }

    public ApiResponse UpdateSettings(UpdatePrivacySettingsRequest model, ClaimsPrincipal userClaims)
    {
        if (model.ForUsername != null)
        {
            if (!userClaims.IsInRole("Admin"))
            {
                return new ApiResponse{Code=403};
            }
            
            var foreignSettings = _privacyRepo.GetUserPrivacySettingsByUsername(model.ForUsername);
            if (foreignSettings == null)
            {
                return new ApiResponse{Code=404, Payload=new {Message="Settings not found."}};
            }

            foreignSettings.AllowUnauthorizedView = model.AllowUnauthorizedView;
            foreignSettings.HideInSearch = model.HideInSearch ?? foreignSettings.HideInSearch;
            foreignSettings.HideProfile = model.HideProfile ?? foreignSettings.HideProfile;
            _privacyRepo.UpdatePrivacySettings(foreignSettings);

            return new ApiResponse{Code=200};
        }

        var settings = _privacyRepo.GetUserPrivacySettingsByUsername(userClaims!.Identity!.Name!);
        if (settings == null)
        {
            return new ApiResponse{Code=404, Payload=new {Message="Settings not found."}};
        }
        settings.AllowUnauthorizedView = model.AllowUnauthorizedView;
        settings.HideInSearch = model.HideInSearch ?? settings.HideInSearch;
        settings.HideProfile = model.HideProfile ?? settings.HideProfile;
        _privacyRepo.UpdatePrivacySettings(settings);
        
        return new ApiResponse{Code=200};
    }
}
