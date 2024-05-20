using ConfigurationService.Database;
using ConfigurationService.Database.Models;

namespace ConfigurationService.Repository;

public class GlobalSettingRepository : IGlobalSettingRepository
{
    private readonly ApplicationContext _db;

    public GlobalSettingRepository(ApplicationContext db)
    {
        _db = db;
    }

    public bool CreateGlobalSetting(GlobalSetting globalSetting)
    {
        _db.GlobalSettings.Add(globalSetting);
        return Save();
    }

    public bool UpdateGlobalSetting(GlobalSetting globalSetting)
    {
        _db.GlobalSettings.Update(globalSetting);
        return Save();
    }

    public bool DeleteGlobalSetting(GlobalSetting globalSetting)
    {
        _db.GlobalSettings.Remove(globalSetting);
        return Save();
    }

        public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public GlobalSetting? GetGlobalSetting(string section, string setting)
    {
        return _db.GlobalSettings.FirstOrDefault(gs => gs.Section == section && gs.Name == setting);
    }

    public GlobalSetting? GetGlobalSettingById(int id)
    {
        return _db.GlobalSettings.FirstOrDefault(gs => gs.Id == id);
    }

    public IQueryable<GlobalSetting> GetGlobalSettings()
    {
        return _db.GlobalSettings;
    }

    public IQueryable<GlobalSetting> GetGlobalSettingsInSection(string section)
    {
        return _db.GlobalSettings.Where(gs => gs.Section == section);
    }
}
