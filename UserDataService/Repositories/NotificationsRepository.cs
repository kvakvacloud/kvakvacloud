using UserDataService.Database;
using UserDataService.Database.Models;

namespace UserDataService.Repositories;

public class NotificationsRepository(ApplicationContext db) : INotificationsRepository
{
    private readonly ApplicationContext _db = db;
    public bool AddNotification(Notification obj)
    {
        _db.Notifications.Add(obj);
        return Save();
    }

    public bool DeleteAllForUsername(string username)
    {
        _db.Notifications.RemoveRange(_db.Notifications.Where(o => o.Username == username));
        return Save();
    }

    public bool DeleteNotification(Notification obj)
    {
        _db.Notifications.Remove(obj);
        return Save();
    }

    public Notification? GetNotificationById(long id)
    {
        return _db.Notifications.FirstOrDefault(o => o.Id == id);
    }

    public IQueryable<Notification> GetNotificationsByUsername(string username)
    {
        return _db.Notifications.Where(o => o.Username == username);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public bool UpdateNotification(Notification obj)
    {
        _db.Notifications.Update(obj);
        return Save();
    }
}