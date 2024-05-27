using System.ComponentModel.DataAnnotations;
using UserDataService.Enums;

namespace UserDataService.Models.Notifications.Requests;

public class AddNotificationRequest
{
    [Required]
    public string ForUsername {get;set;} = null!;
    public string? Subject {get;set;}
    public string? Body {get;set;}
    public NotificationPriority Priority {get;set;} = NotificationPriority.Low;
}