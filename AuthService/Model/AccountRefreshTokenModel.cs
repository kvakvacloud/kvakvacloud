using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Model;

[Obsolete("Не использовать, переходим к автоматической авторизации")]
public class AccountRefreshTokenModel
{
    [Required]
    [FromHeader]
    public string RefreshToken {get;set;} = null!;
}