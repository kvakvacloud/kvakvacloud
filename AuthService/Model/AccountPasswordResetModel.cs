using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountPasswordResetModel
{
    [Required]
    public string ResetCode;
    [Required]
    [MinLength(8)]
    public string NewPassword;
}