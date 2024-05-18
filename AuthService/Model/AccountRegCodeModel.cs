using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountRegCodeModel
{
    [Required]
    public string Code {get;set;}
}