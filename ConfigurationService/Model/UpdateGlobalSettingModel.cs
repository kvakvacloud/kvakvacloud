using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Model;

public class UpdateGlobalSettingModel
{
    [Required]
    public string? Section {get;set;}

    [Required]
    public string? Name {get;set;}

    [Required]
    public string? Value {get;set;}

    [Required]
    [FromHeader]
    public string? AccessToken {get;set;}
}