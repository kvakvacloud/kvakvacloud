using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

public class AccountForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = null!;
}