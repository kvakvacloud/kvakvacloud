using System.ComponentModel.DataAnnotations;

namespace UserDataService.Database.Models;

public class PrivacySettings
{
    [Key]
    public string Username {get;set;} = null!;
    public bool HideInSearch {get;set;} = false;
    public bool HideProfile {get;set;} = false;
    public bool AllowUnauthorizedView {get;set;} = false;
    //public bool AllowMessages {get;set;} = false;
}