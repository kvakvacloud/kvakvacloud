using System.ComponentModel.DataAnnotations;
using AuthService.Validation.Attributes;

namespace AuthService.Model;

public class AccountFinishRegistrationRequest
{
    [Required]
    [ValidGuid]
    public string Code {get;set;} = null!;

    [Required]
    public string Username {get;set;} = null!;

    [Required]
    [MinLength(8)]
    public string Password {get;set;} = null!;



}