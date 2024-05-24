using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountLoginRequest
{
    [Required]
    public string Username {get;set;} = null!;

    [Required]
    public string Password {get;set;} = null!;
}