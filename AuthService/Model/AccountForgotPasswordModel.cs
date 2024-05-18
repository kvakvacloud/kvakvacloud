using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string? Email;
}