using AuthService.Database.Models;
namespace AuthService.Utils;

public interface IJwtUtils {
    public string GenerateJwtToken(User user);
    public bool ValidateJwtToken(string jwt);
}