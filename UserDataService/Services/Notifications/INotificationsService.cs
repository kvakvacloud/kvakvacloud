using System.Security.Claims;
using UserDataService.Models.General.Responses;
using UserDataService.Models.Notifications.Requests;

namespace UserDataService.Services.Notifications;

public interface INotificationsService
{
    public ApiResponse GetAll(GetNotificationsRequest model, ClaimsPrincipal userClaims);
    public ApiResponse Read(MarkNotificationAsReadRequest model, ClaimsPrincipal userClaims);
    public ApiResponse Delete(DeleteNotificationRequest model, ClaimsPrincipal userClaims);
    public ApiResponse ClealAll(ClearNotificationsRequest model, ClaimsPrincipal userClaims);
}