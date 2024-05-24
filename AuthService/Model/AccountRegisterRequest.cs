using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountRegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = null!;
}