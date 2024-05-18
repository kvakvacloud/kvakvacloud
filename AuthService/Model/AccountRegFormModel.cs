using System.ComponentModel.DataAnnotations;
using AuthService.Validation.Attributes;

namespace AuthService.Model;

public class AccountRegFormModel
{
    [Required]
    [ValidGuid]
    public string? Code {get;set;}

    [Required]
    public string? Username {get;set;}

    [Required]
    [MinLength(8)]
    public string? Password {get;set;}



}