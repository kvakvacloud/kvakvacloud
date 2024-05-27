using UserDataService.Database.Models;

namespace UserDataService.Repositories;

public interface IPrivacySettingsRepository
{
    public bool AddPrivacySettings(PrivacySettings obj);
    public bool UpdatePrivacySettings(PrivacySettings obj);
    public bool DeletePrivacySettings(PrivacySettings obj);
    public PrivacySettings? GetUserProfileByUsername(string username);
    public IQueryable<PrivacySettings> GetPrivacySettings();
    public bool Save();
}