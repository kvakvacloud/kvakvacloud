using UserDataService.Database.Models;

namespace UserDataService.Repositories;

public interface IUserProfileRepository
{
    public bool AddUserProfile(UserProfile obj);
    public bool UpdateUserProfile(UserProfile obj);
    public bool DeleteUserProfile(UserProfile obj);
    public UserProfile? GetUserProfileById(long id);
    public UserProfile? GetUserProfileByUsername(string username);
    public IQueryable<UserProfile> GetUserProfiles();
    public bool Save();
}