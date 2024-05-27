using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UserDataService.Models.Requests;

public class UserDataUpdateProfileRequest
{
    [FromQuery]
    public string? ToUsername {get;set;}
    [FromQuery]
    public long? ToUserId {get;set;}
    private string? _firstName;
    private string? _lastName;
    private string? _about;
    public string? FirstName
    {
        get {return _firstName;}
        set {_firstName = value?.Trim();}
    }
    public string? LastName
    {
        get {return _lastName;}
        set {_lastName = value?.Trim();}
    }
    public string? About
    {
        get {return _about;}
        set {_about = value?.Trim();}
    }
    [Phone]
    public string? Phone {get;set;}
    [EmailAddress]
    public string? DisplayEmail {get;set;}
}