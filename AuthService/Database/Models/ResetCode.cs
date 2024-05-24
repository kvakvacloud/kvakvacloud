using System.ComponentModel.DataAnnotations;
using AuthService.Validation.Attributes;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database.Models;

[Index(nameof(Code), IsUnique = true)]
public class ResetCode
{
    [Key]
    public int Id {get;set;}
    [Required]
    [ValidGuid]
    public Guid Code {get;set;}
    [Required]
    public int UserId {get;set;}
    [Required]
    public User User {get;set;} = null!;
    [Required]
    public DateTime ValidUntil {get;set;}
    public bool Used {get;set;}=false;
    public DateTime? WhenWasUsed {get;set;}
}