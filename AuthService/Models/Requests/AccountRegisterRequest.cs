using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

public class AccountRegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = null!;
}