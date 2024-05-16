using AuthService.Database;
using AuthService.Database.Models;

namespace AuthService.Repository;

public interface IUserRepository
{
    IQueryable<User> GetUsers();
    User? GetUserById(int id);
    User? GetUserByUsername(string username);
    User? GetUserByEmail(string email);
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(User user);
    bool Save();
}