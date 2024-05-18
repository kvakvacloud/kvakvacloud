using AuthService.Database;
using AuthService.Database.Models;

namespace AuthService.Repository;

public interface IRegistrationCodeRepository
{
    public IQueryable<RegistrationCode> GetRegistrationCodes();
    public RegistrationCode? GeRegistrationCodeById(int id);
    public RegistrationCode? GetRegistrationCodeByCode(Guid code);
    public bool CreateRegistrationCode(RegistrationCode regcode);
    public bool UpdateRegistrationCode(RegistrationCode regcode);
    public bool DeleteRegistrationCode(RegistrationCode regcode);
    public bool Save();
}