using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database.Models;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User {
    [Key]
    public int Id {get;set;}
    [NotNull]
    public required string Username {get;set;}
    [EmailAddress]
    public string? Email {get;set;}
    [NotNull] [MinLength(8)]
    public required string Password {get;set;}
    [NotNull]
    public DateTime RegisrationDate {get;set;}
    [NotNull]
    public DateTime PasswordChangeDate {get;set;}
    public bool IsSuper {get;set;}
    public bool ForcePasswordChange {get;set;}
}