using Microsoft.EntityFrameworkCore;
namespace FilesystemService.Database;

public class ApplicationContext : DbContext
{
    #pragma warning disable CS8618
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
	
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}