using Microsoft.AspNetCore.Mvc;

namespace UserDataService.Models.Notifications.Requests;

public class ClearNotificationsRequest
{
    [FromQuery]
    public string ForUsername {get;set;} = null!;
}