using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountChangePasswordModel
{
    [Required]
    public string Token;
    [Required]
    public string OldPassword;
    [Required]
    [MinLength(8)]
    public string NewPassword;
}