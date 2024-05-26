using AuthService.Database.Models;

namespace AuthService.Repositories;

public interface IRevokedPasswordRepository
{
    public IQueryable<RevokedPassword> GetRevokedPasswords();
    public RevokedPassword? GeRevokedPasswordById(long id);
    public IQueryable<RevokedPassword>? GeRevokedPasswordsByUserId(long userId);
    public IQueryable<RevokedPassword>? GeRevokedPasswordsByUsername(string username);
    public bool CreateRevokedPassword(RevokedPassword password);
    public bool UpdateRevokedPassword(RevokedPassword password);
    public bool DeleteRevokedPassword(RevokedPassword password);
    public bool Save();
}