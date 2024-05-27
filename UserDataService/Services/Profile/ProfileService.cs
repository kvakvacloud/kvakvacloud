using UserDataService.Models.General.Responses;
using UserDataService.Models.Profile.Requests;
using UserDataService.Repositories;

namespace UserDataService.Services.Profile;

public class ProfileService(IUserProfileRepository profileRepo) : IProfileService
{
    private readonly IUserProfileRepository _profileRepo = profileRepo;
    public ApiResponse Get(GetProfileRequest model)
    {
        throw new NotImplementedException();
    }

    public ApiResponse Update(UpdateProfileRequest model)
    {
        throw new NotImplementedException();
    }
}