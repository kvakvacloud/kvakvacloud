using FilesystemService.Database;
using FilesystemService.Database.Models;

namespace FilesystemService.Repositories;

public class SharedFileRepository(ApplicationContext db) : ISharedFileRepository
{
    private readonly ApplicationContext _db = db;
    public bool AddSharedFile(SharedFile obj)
    {
        _db.SharedFiles.Add(obj);
        return Save();
    }

    public bool DeleteSharedFile(SharedFile obj)
    {
        _db.SharedFiles.Remove(obj);
        return Save();
    }

    public SharedFile? GetSharedFileById(long id)
    {
        return _db.SharedFiles.FirstOrDefault(o => o.Id == id);
    }

    public IQueryable<SharedFile> GetSharedFiles()
    {
        return _db.SharedFiles;
    }

    public IQueryable<SharedFile>? GetSharedFilesByUsername(string username)
    {
        return _db.SharedFiles.Where(o => o.OwnerUsername == username);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdateSharedFile(SharedFile obj)
    {
        _db.SharedFiles.Update(obj);
        return Save();
    }
}