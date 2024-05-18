using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Model;

public class AccountRefreshTokenModel
{
    [Required]
    [FromHeader]
    public string? RefreshToken {get;set;} 
}