using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database.Models;

public class RevokedPassword {
    [Key]
    public int Id {get;set;}
    [NotNull]
    public int UserId {get;set;}
    public required User User {get;set;}
    [NotNull]
    public string? Password {get;set;}
}