using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Database.Models;

public class Ban {
    [Key]
    public int Id {get;set;}
    [NotNull]
    public int UserId {get;set;}
    public required User User {get;set;}
    [NotNull]
    public DateTime When {get;set;}
    public DateTime Until {get;set;}
    public string? Reason {get;set;}
    public bool Resolvable {get;set;}
}