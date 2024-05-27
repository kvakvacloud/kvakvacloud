using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UserDataService.Models.Profile.Requests;

public class GetProfileRequest
{
    [FromQuery]
    public string? ForUsername {get;set;}
}