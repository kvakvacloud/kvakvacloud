using Microsoft.EntityFrameworkCore;
using AuthService.Database.Models;
namespace AuthService.Database;

public class ApplicationContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Ban>? Bans { get; set; }
    public DbSet<RevokedPassword>? RevokedPasswords { get; set; }

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
	
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var Hostname=Environment.GetEnvironmentVariable("DB_HOSTNAME") ?? "authdb";
        var Port=Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        var Name=Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
        var Username=Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
        var Password=Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
        optionsBuilder.UseNpgsql($"Server={Hostname}:{Port};Database={Name};Uid={Username};Pwd={Password};");
    }
}