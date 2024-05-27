using System.Data.Common;
using UserDataService.Database;
using UserDataService.Database.Models;

namespace UserDataService.Repositories;

public class PrivacySettingsRepository(ApplicationContext db) : IPrivacySettingsRepository
{
    private readonly ApplicationContext _db = db;
    public bool AddPrivacySettings(PrivacySettings obj)
    {
        _db.PrivacySettings.Add(obj);
        return Save();
    }

    public bool DeletePrivacySettings(PrivacySettings obj)
    {
        _db.PrivacySettings.Remove(obj);
        return Save();
    }

    public IQueryable<PrivacySettings> GetPrivacySettings()
    {
        return _db.PrivacySettings;
    }

    public PrivacySettings? GetUserPrivacySettingsByUsername(string username)
    {
        return _db.PrivacySettings.FirstOrDefault(o => o.Username == username);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdatePrivacySettings(PrivacySettings obj)
    {
        _db.PrivacySettings.Update(obj);
        return Save();
    }
}