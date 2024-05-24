using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountChangePasswordModel
{
    [Required]
    public string AccessToken {get;set;} = null!;
    [Required]
    public string OldPassword {get;set;} = null!;
    [Required]
    [MinLength(8)]
    public string NewPassword {get;set;} = null!;
}