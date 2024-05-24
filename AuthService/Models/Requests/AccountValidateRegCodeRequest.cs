using System.ComponentModel.DataAnnotations;
using AuthService.Validation.Attributes;

namespace AuthService.Models.Requests;

public class AccountValidateRegCodeRequest
{
    [Required]
    [ValidGuid]
    public string Code {get;set;} = null!;
}