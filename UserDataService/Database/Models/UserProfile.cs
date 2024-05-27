using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UserDataService.Database.Models;

[Index(nameof(Username), IsUnique = true)]
public class Profile {
    private string? _firstName;
    private string? _lastName;
    private string? _about;
    [Key]
    public int Id {get;set;}
    [Required]
    public string Username {get;set;} = null!;
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
    public string? Picture {get;set;} = null!;
    [Phone]
    public string? Phone {get;set;} = null!;
    [EmailAddress]
    public string? DisplayEmail {get;set;} = null!;
    public bool IsHidden {get;set;} = false;
    public bool IsHiddenInSearch {get;set;} = false;
}