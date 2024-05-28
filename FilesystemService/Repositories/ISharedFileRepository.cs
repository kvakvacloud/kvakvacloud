using FilesystemService.Database.Models;

namespace FilesystemService.Repositories;

public interface ISharedFileRepository
{
    public bool AddSharedFile(SharedFile obj);
    public bool UpdateSharedFile(SharedFile obj);
    public bool DeleteSharedFile(SharedFile obj);
    public SharedFile? GetSharedFileById(long id);
    public IQueryable<SharedFile> GetSharedFiles();
    public IQueryable<SharedFile>? GetSharedFilesByUsername(string username);
    public bool Save();
}