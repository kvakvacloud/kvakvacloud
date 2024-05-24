using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountRegisterModel
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = null!;
}