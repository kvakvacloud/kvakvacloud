using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountRegFormModel
{
    [Required]
    public string Code {get;set;}

    [Required]
    public string Username {get;set;}

    [Required]
    public string Password {get;set;}



}