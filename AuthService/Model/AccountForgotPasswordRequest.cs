using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = null!;
}