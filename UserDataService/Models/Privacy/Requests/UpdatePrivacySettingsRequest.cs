using Microsoft.AspNetCore.Mvc;

namespace UserDataService.Models.Privacy.Requests;

public class UpdatePrivacySettingsRequest
{
    [FromQuery]
    public string? ForUsername {get;set;}
    public bool? HideInSearch {get;set;}
    public bool? HideProfile {get;set;}
    public bool AllowUnauthorizedView {get;set;}
}