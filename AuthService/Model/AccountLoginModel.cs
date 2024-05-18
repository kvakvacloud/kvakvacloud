using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountLoginModel
{
    [Required]
    public string Username {get;set;}="";

    [Required]
    public string Password {get;set;}="";
}