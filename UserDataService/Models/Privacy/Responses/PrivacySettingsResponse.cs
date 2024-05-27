namespace UserDataService.Models.Privacy.Responses;

public class PrivacySettingsResponse
{
    public bool HideInSearch {get;set;} = false;
    public bool HideProfile {get;set;} = false;
    public bool AllowUnauthorizedView {get;set;} = false;
}