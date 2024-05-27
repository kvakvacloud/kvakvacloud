using System.Security.Claims;
using UserDataService.Models.General.Responses;
using UserDataService.Models.Profile.Requests;
using UserDataService.Models.Profile.Responses;
using UserDataService.Repositories;

namespace UserDataService.Services.Profile;

public class ProfileService(IUserProfileRepository profileRepo, IPrivacySettingsRepository privacyRepo) : IProfileService
{
    private readonly IUserProfileRepository _profileRepo = profileRepo;
    private readonly IPrivacySettingsRepository _privacyRepo = privacyRepo;
    public ApiResponse Get(GetProfileRequest model, ClaimsPrincipal userClaims)
    {
        if (model.ForUsername != null)
        {
            var foreignProfile = _profileRepo.GetUserProfileByUsername(model.ForUsername);
            if (foreignProfile == null)
            {
                return new ApiResponse{Code=404, Payload=new {Message="Profile not found."}};
            }

            var privacySettings = _privacyRepo.GetUserPrivacySettingsByUsername(model.ForUsername);
            if (privacySettings == null)
            {
                return new ApiResponse{Code=500, Payload=new {Message="Failed to load privacy settings for that profile."}};
            }

            bool hideConfidential = privacySettings.HideProfile && !userClaims.IsInRole("Admin");

            ProfileResponse foreignProfileResponse = new()
            {
                Username = foreignProfile.Username,
                FirstName = foreignProfile.FirstName,
                LastName = foreignProfile.LastName,
                About = foreignProfile.About,
                Phone = hideConfidential ? null : foreignProfile.Phone,
                Picture = hideConfidential ? null : foreignProfile.Picture,
                IsPrivate = privacySettings.HideProfile
            };

            return new ApiResponse{Code=200, Payload=foreignProfileResponse};
        }

        var profile = _profileRepo.GetUserProfileByUsername(userClaims!.Identity!.Name!);
        if (profile == null)
        {
            return new ApiResponse{Code=404, Payload=new {Message="Failed to load your profile."}};
        }

        ProfileResponse profileResponse = new()
        {
            Username = profile.Username,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            About = profile.About,
            Phone = profile.Phone,
            Picture = profile.Picture
        };

        return new ApiResponse{Code=200, Payload=profileResponse};
        
    }

    public ApiResponse Update(UpdateProfileRequest model, ClaimsPrincipal userClaims)
    {
        if (model.ForUsername != null)
        {
            if (!userClaims.IsInRole("Admin"))
            {
                return new ApiResponse{Code=403};
            }
            var foreignProfile = _profileRepo.GetUserProfileByUsername(model.ForUsername);
            if (foreignProfile == null)
            {
                return new ApiResponse{Code=404, Payload=new {Message="Profile not found."}};
            }
            foreignProfile.FirstName = model.FirstName ?? foreignProfile.FirstName;
            foreignProfile.LastName = model.LastName ?? foreignProfile.LastName;
            foreignProfile.About = model.About ?? foreignProfile.About;
            foreignProfile.Phone = model.Phone ?? foreignProfile.Phone;
            _profileRepo.UpdateUserProfile(foreignProfile);
            return new ApiResponse{Code=200};
        }

        var profile = _profileRepo.GetUserProfileByUsername(userClaims!.Identity!.Name!);
        if (profile == null)
        {
            return new ApiResponse{Code=500, Payload=new {Message="Failed to load your profile."}};
        }
        profile.FirstName = model.FirstName ?? profile.FirstName;
        profile.LastName = model.LastName ?? profile.LastName;
        profile.About = model.About ?? profile.About;
        profile.Phone = model.Phone ?? profile.Phone;
        _profileRepo.UpdateUserProfile(profile);
        return new ApiResponse{Code=200};
    }
}