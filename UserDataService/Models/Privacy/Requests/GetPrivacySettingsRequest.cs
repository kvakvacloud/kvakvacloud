using Microsoft.AspNetCore.Mvc;

namespace UserDataService.Models.Privacy.Requests;

public class GetPrivacySettingsRequest
{
    [FromQuery]
    public string? ForUsername {get;set;}
}