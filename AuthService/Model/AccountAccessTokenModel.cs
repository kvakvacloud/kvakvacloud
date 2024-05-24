using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

[Obsolete("Не использовать, переходим к автоматической авторизации")]
public class AccountAccessTokenModel
{
    [Required]
    public string AccessToken {get;set;} = null!;
}