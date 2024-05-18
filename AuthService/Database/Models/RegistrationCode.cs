using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database.Models;

[Index(nameof(Code), IsUnique = true)]
public class RegistrationCode
{
    [Key]
    public int Id {get;set;}
    [Required]
    public Guid Code {get;set;}
    [Required]
    public int UserId {get;set;}
    [Required]
    public User User {get;set;}
    [Required]
    public DateTime ValidUntil {get;set;}
    public bool? Used {get;set;}
    public DateTime? WhenWasUsed {get;set;}
}