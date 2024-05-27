using UserDataService.Models.General.Responses;
using UserDataService.Models.Notifications.Requests;

namespace UserDataService.Services.Notifications;

public interface INotificationsService
{
    public ApiResponse GetAll(GetNotificationsRequest model);
    public ApiResponse Read(MarkNotificationAsReadRequest model);
    public ApiResponse Delete(DeleteNotificationRequest model);
    public ApiResponse ClealAll(ClearNotificationsRequest model);
}