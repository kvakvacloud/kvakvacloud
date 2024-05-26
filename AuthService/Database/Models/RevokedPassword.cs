using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database.Models;

public class RevokedPassword {
    [Key]
    public int Id {get;set;}
    [Required]
    public User User {get;set;} = null!;
    [Required]
    public string Password {get;set;} = null!;
}