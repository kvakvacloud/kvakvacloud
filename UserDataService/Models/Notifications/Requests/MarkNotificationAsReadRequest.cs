using System.ComponentModel.DataAnnotations;
using UserDataService.Enums;

namespace UserDataService.Models.Notifications.Requests;

public class MarkNotificationAsReadRequest
{
    [Required]
    public long Id {get;set;} 
}