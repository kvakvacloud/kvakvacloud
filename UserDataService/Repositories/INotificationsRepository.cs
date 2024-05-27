using UserDataService.Database.Models;

namespace UserDataService.Repositories;

public interface INotificationsRepository
{
    public bool AddNotification(Notification obj);
    public bool UpdateNotification(Notification obj);
    public bool DeleteNotification(Notification obj);
    public Notification? GetNotificationById(long id);
    public IQueryable<Notification> GetNotificationsByUsername(string username);
    public bool Save();
}