using FilesystemService.Database;
using FilesystemService.Database.Models;

namespace FilesystemService.Repositories;

public class SharedDirectoryRepository(ApplicationContext db) : ISharedDirectoryRepository
{
    private readonly ApplicationContext _db = db;
    public bool AddSharedDirectory(SharedDirectory obj)
    {
        _db.SharedDirectories.Add(obj);
        return Save();
    }

    public bool DeleteSharedDirectory(SharedDirectory obj)
    {
        _db.SharedDirectories.Remove(obj);
        return Save();
    }

    public SharedDirectory? GetSharedDirectoryById(long id)
    {
        return _db.SharedDirectories.FirstOrDefault(o => o.Id == id);
    }

    public IQueryable<SharedDirectory> GetSharedDirectories()
    {
        return _db.SharedDirectories;
    }

    public IQueryable<SharedDirectory>? GetSharedDirectoriesByUsername(string username)
    {
        return _db.SharedDirectories.Where(o => o.OwnerUsername == username);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdateSharedDirectory(SharedDirectory obj)
    {
        _db.SharedDirectories.Update(obj);
        return Save();
    }
}