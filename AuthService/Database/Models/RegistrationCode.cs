using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
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
    [EmailAddress]
    public string Email {get;set;}
    [Required]
    public DateTime ValidUntil {get;set;}
    public bool Used {get;set;} = false;
    public DateTime WhenWasUsed {get;set;}
}