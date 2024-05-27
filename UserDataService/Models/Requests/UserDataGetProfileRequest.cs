using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UserDataService.Models.Requests;

public class UserDataGetProfileRequest
{
    [FromQuery]
    public int? UserId {get;set;}
    [FromQuery]
    public string? Username {get;set;}
}