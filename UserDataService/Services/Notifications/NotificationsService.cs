using System.Security.Claims;
using UserDataService.Models.General.Responses;
using UserDataService.Models.Notifications.Requests;
using UserDataService.Models.Notifications.Responses;
using UserDataService.Repositories;

namespace UserDataService.Services.Notifications;

public class NotificationsService(INotificationsRepository notificationsRepo) : INotificationsService
{
    private readonly INotificationsRepository _notificationsRepo = notificationsRepo;
    public ApiResponse ClealAll(ClearNotificationsRequest model, ClaimsPrincipal userClaims)
    {
        if (model.ForUsername != null)
        {
            if (!userClaims.IsInRole("Admin"))
            {
                return new ApiResponse{Code=403};
            }
            _notificationsRepo.DeleteAllForUsername(model.ForUsername);
            return new ApiResponse{Code=200};
        }
        string username = userClaims!.Identity!.Name!;
        _notificationsRepo.DeleteAllForUsername(username);
        return new ApiResponse{Code=200};
    }

    public ApiResponse Delete(DeleteNotificationRequest model, ClaimsPrincipal userClaims)
    {
        if (model.ForUsername != null)
        {
            if (!userClaims.IsInRole("Admin"))
            {
                return new ApiResponse{Code=403};
            }
            var foreginNotification = _notificationsRepo
            .GetNotificationsByUsername(model.ForUsername)
            .FirstOrDefault(o => o.Id == model.Id);

            if (foreginNotification == null)
            {
                return new ApiResponse{Code=404, Payload=new {Message="Notification not found."}};
            }
            return new ApiResponse{Code=200};
        }
        var notification = _notificationsRepo
        .GetNotificationsByUsername(userClaims!.Identity!.Name!)
        .FirstOrDefault(o => o.Id == model.Id);

        if (notification == null)
        {
            return new ApiResponse{Code=404, Payload=new {Message="Notification not found."}};
        }

        _notificationsRepo.DeleteNotification(notification);
        return new ApiResponse{Code=200};
    }

    public ApiResponse GetAll(GetNotificationsRequest model, ClaimsPrincipal userClaims)
    {
        var notifications = _notificationsRepo
        .GetNotificationsByUsername(userClaims!.Identity!.Name!)
        .Select(o => new NotificationModel
        {
            Id = o.Id,
            Subject = o.Subject,
            Body = o.Body,
            Read = o.Read,
            Priority = o.Priority
        });
        return new ApiResponse{Code=200, Payload=notifications};
    }

    public ApiResponse Read(MarkNotificationAsReadRequest model, ClaimsPrincipal userClaims)
    {
        var notification = _notificationsRepo
        .GetNotificationsByUsername(userClaims!.Identity!.Name!)
        .FirstOrDefault(o => o.Id == model.Id);
        if (notification == null)
        {
            return new ApiResponse{Code=404, Payload=new {Message="Notification not found."}};
        }
        notification.Read = true;
        _notificationsRepo.UpdateNotification(notification);
        return new ApiResponse{Code=200};
    }
}
