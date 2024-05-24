using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

public class AccountPasswordResetRequest
{
    [Required]
    public string ResetCode {get;set;} = null!;
    [Required]
    [MinLength(8)]
    public string NewPassword {get;set;} = null!;
}