using System.ComponentModel.DataAnnotations;

namespace AuthService.Model;

public class AccountChangePasswordModel
{
    [Required]
    public string Token {get;set;}="";
    [Required]
    public string OldPassword {get;set;}="";
    [Required]
    [MinLength(8)]
    public string NewPassword {get;set;}="";
}