using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Model;

public class RequestServiceTokenModel
{
    [Required]
    [FromHeader]
    public string? Host {get;set;}

    [Required]
    [FromHeader(Name = "X-Real-IP")]
    public string? XRealIP {get;set;}
}