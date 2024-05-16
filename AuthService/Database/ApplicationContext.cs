using Microsoft.EntityFrameworkCore;
using AuthService.Database.Models;
namespace AuthService.Database;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Ban> Bans { get; set; }
    public DbSet<RevokedPassword> RevokedPasswords { get; set; }

    #pragma warning disable CS8618
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
	
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}