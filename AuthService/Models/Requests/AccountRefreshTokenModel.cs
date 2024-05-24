using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Models.Requests;

[Obsolete("Не использовать, переходим к автоматической авторизации")]
public class AccountRefreshTokenModel
{
    [Required]
    [FromHeader]
    public string RefreshToken {get;set;} = null!;
}