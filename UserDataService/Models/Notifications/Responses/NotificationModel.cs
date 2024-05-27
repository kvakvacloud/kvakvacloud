using UserDataService.Enums;

namespace UserDataService.Models.Notifications.Responses;

public class NotificationModel
{
    public long Id {get;set;}
    public string? Subject {get;set;}
    public string Body {get;set;} = null!;
    public bool Read {get;set;} = false;
    public NotificationPriority Priority {get;set;} = NotificationPriority.Medium; 
}