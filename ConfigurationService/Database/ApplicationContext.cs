using ConfigurationService.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace ConfigurationService.Database;

public class ApplicationContext : DbContext
{
    public DbSet<GlobalSetting>? GlobalSettings { get; set; }
    #pragma warning disable CS8618
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

        Database.EnsureCreated();
    }
	
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}