using AuthService.Database;
using AuthService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories;

public class RevokedPasswordRepository(ApplicationContext db) : IRevokedPasswordRepository
{
    private readonly ApplicationContext _db = db;
    public bool CreateRevokedPassword(RevokedPassword password)
    {
        _db.RevokedPasswords.Add(password);
        return Save();
    }

    public bool DeleteRevokedPassword(RevokedPassword password)
    {
        _db.RevokedPasswords.Remove(password);
        return Save();
    }

    public RevokedPassword? GeRevokedPasswordById(long id)
    {
        return _db.RevokedPasswords.FirstOrDefault(p => p.Id == id);
    }

    public IQueryable<RevokedPassword>? GeRevokedPasswordsByUserId(long userId)
    {
        return _db.RevokedPasswords.Include(rp => rp.User).Where(rp => rp.User.Id == userId);
    }

    public IQueryable<RevokedPassword>? GeRevokedPasswordsByUsername(string username)
    {
        return _db.RevokedPasswords.Include(rp => rp.User).Where(rp => rp.User.Username == username);
    }

    public IQueryable<RevokedPassword> GetRevokedPasswords()
    {
        return _db.RevokedPasswords;
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdateRevokedPassword(RevokedPassword password)
    {
        _db.RevokedPasswords.Update(password);
        return Save();
    }
}