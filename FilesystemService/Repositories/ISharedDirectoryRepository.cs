using FilesystemService.Database.Models;

namespace FilesystemService.Repositories;

public interface ISharedDirectoryRepository
{
    bool AddSharedDirectory(SharedDirectory obj);
    bool UpdateSharedDirectory(SharedDirectory obj);
    bool DeleteSharedDirectory(SharedDirectory obj);
    IQueryable<SharedDirectory> GetSharedDirectories();
    IQueryable<SharedDirectory>? GetSharedDirectoriesByUsername(string username);
    bool Save();
}