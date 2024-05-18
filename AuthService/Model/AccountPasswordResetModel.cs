using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountPasswordResetModel
{
    [Required]
    public string? ResetCode {get;set;}
    [Required]
    [MinLength(8)]
    public string? NewPassword {get;set;}
}