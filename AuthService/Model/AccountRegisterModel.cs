using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountRegisterModel
{
    [Required]
    public string? Email {get;set;}

    [Required] [MinLength(8)]
    public string? Password {get;set;}
}