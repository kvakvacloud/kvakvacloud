namespace UserDataService.Models.Profile.Responses;

public class ProfileResponse
{
    public string? Username {get;set;}
    public string? FirstName {get;set;}
    public string? LastName {get;set;}
    public string? About {get;set;}
    public string? Phone {get;set;}
    public string? Picture {get;set;}
    public bool IsPrivate {get;set;}
}