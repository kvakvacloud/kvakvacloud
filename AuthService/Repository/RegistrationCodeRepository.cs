using AuthService.Database;
using AuthService.Database.Models;

namespace AuthService.Repository;

public class RegistrationCodeRepository : IRegistrationCodeRepository
{
    private readonly ApplicationContext _db;
    public RegistrationCodeRepository(ApplicationContext db)
    {
        _db = db;
    }
    public IQueryable<RegistrationCode> GetRegistrationCodes()
    {
        return _db.RegistrationCodes.AsQueryable();
    }
    public RegistrationCode? GeRegistrationCodeById(int id)
    {
        return _db.RegistrationCodes.FirstOrDefault(rc => rc.Id == id);
    }
    public RegistrationCode? GetRegistrationCodeByCode(Guid code)
    {
        return _db.RegistrationCodes.FirstOrDefault(rc => rc.Code == code);
    }
    public bool CreateRegistrationCode(RegistrationCode regcode)
    {
        _db.RegistrationCodes.Add(regcode);
        return Save();
    }
    public bool UpdateRegistrationCode(RegistrationCode regcode)
    {
        _db.RegistrationCodes.Update(regcode);
        return Save();
    }
    public bool DeleteRegistrationCode(RegistrationCode regcode)
    {
        _db.RegistrationCodes.Remove(regcode);
        return Save();
    }
    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
}