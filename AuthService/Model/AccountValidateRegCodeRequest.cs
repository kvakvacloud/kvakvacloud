using System.ComponentModel.DataAnnotations;
using AuthService.Validation.Attributes;

namespace AuthService.Model;

public class AccountValidateRegCodeRequest
{
    [Required]
    [ValidGuid]
    public string Code {get;set;} = null!;
}