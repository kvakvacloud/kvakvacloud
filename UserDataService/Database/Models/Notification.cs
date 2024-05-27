using System.ComponentModel.DataAnnotations;
using UserDataService.Enums;

namespace UserDataService.Database.Models;

public class Notification
{
    [Key]
    public long Id {get;set;}
    [Required]
    public string Username {get;set;} = null!;
    public string? Subject {get;set;}
    [Required]
    public string Body {get;set;} = null!;
    [Required]
    public bool Read {get;set;} = false;
    [Required]
    public NotificationPriority Priority {get;set;} = NotificationPriority.Medium; 
}