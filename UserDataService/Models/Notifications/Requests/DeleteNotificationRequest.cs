using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UserDataService.Models.Notifications.Requests;

public class DeleteNotificationRequest
{
    [FromQuery]
    public string ForUsername {get;set;} = null!;
    [Required]
    public long Id {get;set;}
}