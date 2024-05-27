using UserDataService.Database;
using UserDataService.Database.Models;

namespace UserDataService.Repositories;

public class UserProfileRepository(ApplicationContext db) : IUserProfileRepository
{
    private readonly ApplicationContext _db = db;
    public bool AddUserProfile(Profile obj)
    {
        _db.UserProfiles.Add(obj);
        return Save();
    }

    public bool DeleteUserProfile(Profile obj)
    {
        _db.UserProfiles.Remove(obj);
        return Save();
    }

    public Profile? GetUserProfileById(long id)
    {
        return _db.UserProfiles.FirstOrDefault(o => o.Id == id);
    }

    public IQueryable<Profile> GetUserProfiles()
    {
        return _db.UserProfiles;
    }

    public Profile? GetUserProfileByUsername(string username)
    {
        return _db.UserProfiles.FirstOrDefault(o => o.Username == username);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdateUserProfile(Profile obj)
    {
        _db.UserProfiles.Update(obj);
        return Save();
    }
}
