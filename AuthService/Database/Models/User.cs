using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database.Models;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User {
    [Key]
    public int Id {get;set;}
    [Required]
    [MinLength(3)]
    public string Username {get;set;} = null!;
    [Required]
    [EmailAddress]
    public string Email {get;set;} = null!;
    [Required]
    [MinLength(8)]
    public string Password {get;set;} = null!;
    [Required]
    public DateTime RegistrationDate {get;set;}
    [Required]
    public DateTime PasswordChangeDate {get;set;}
    public bool? IsSuper {get;set;}=false;
    public bool? ForcePasswordChange {get;set;}=false;
}