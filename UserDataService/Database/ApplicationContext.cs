using Microsoft.EntityFrameworkCore;
using UserDataService.Database.Models;
namespace UserDataService.Database;

public class ApplicationContext : DbContext
{
    public DbSet<Profile> UserProfiles { get; set; }
    public DbSet<PrivacySettings> PrivacySettings {get;set;}
    public DbSet<Notification> Notifications {get;set;}

    #pragma warning disable CS8618
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
	
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}