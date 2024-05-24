using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountAccessTokenModel
{
    [Required]
    public string AccessToken {get;set;} = null!;
}