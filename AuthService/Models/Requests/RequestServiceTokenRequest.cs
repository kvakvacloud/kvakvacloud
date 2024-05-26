using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Models.Requests;

public class RequestServiceTokenRequest
{
    [Required]
    [FromHeader]
    public string Host {get;set;} = null!;
    [Required]
    [FromHeader(Name = "X-Real-IP")]
    public string XRealIP {get;set;} = null!;
    [Required]
    [FromHeader(Name = "X-Forwarded-For")]
    public string XForwardedFor {get;set;} = null!;
    [Required]
    [FromHeader(Name = "X-Forwarded-Proto")]
    public string XForwardedProto {get;set;} = null!;
}