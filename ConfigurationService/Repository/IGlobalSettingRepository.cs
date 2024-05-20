using ConfigurationService.Database.Models;

namespace ConfigurationService.Repository;

public interface IGlobalSettingRepository
{
    public bool CreateGlobalSetting(GlobalSetting globalSetting);
    public bool UpdateGlobalSetting(GlobalSetting globalSetting);
    public bool DeleteGlobalSetting(GlobalSetting globalSetting);
    public bool Save();
    public IQueryable<GlobalSetting> GetGlobalSettings();
    public IQueryable<GlobalSetting> GetGlobalSettingsInSection(string section);
    public GlobalSetting? GetGlobalSetting(string section, string setting);
    public GlobalSetting? GetGlobalSettingById(int id);
}