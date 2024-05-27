using UserDataService.Database.Models;

namespace UserDataService.Repositories;

public interface IUserProfileRepository
{
    public bool AddUserProfile(Profile obj);
    public bool UpdateUserProfile(Profile obj);
    public bool DeleteUserProfile(Profile obj);
    public Profile? GetUserProfileById(long id);
    public Profile? GetUserProfileByUsername(string username);
    public IQueryable<Profile> GetUserProfiles();
    public bool Save();
}