using System.ComponentModel.DataAnnotations;
using UserDataService.Enums;

namespace UserDataService.Models.Notifications.Requests;

public class MarkNotificationAsReadRequest
{
    public string ForUsername {get;set;} = null!;
    [Required]
    public long Id {get;set;}
    
}