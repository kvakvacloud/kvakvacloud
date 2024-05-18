using AuthService.Database.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Repository;

namespace AuthService.Utils;

public class JwtUtils {

    private readonly UserRepository _userRepo;

    static readonly string secret = Environment.GetEnvironmentVariable("AUTH_JWT_SECRET") ?? GenerateTempSecret();
    static readonly int expireMinutes = Int32.Parse(Environment.GetEnvironmentVariable("AUTH_JWT_EXPIRE") ?? "5");

    public JwtUtils(UserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    private static string GenerateTempSecret()
    {
        Console.WriteLine("Warning: no Jwt secret is specified, using a random secret instead. "+
        "To prevent token invalidation on restart, specify a secret via AUTH_JWT_SECRET variable for AuthService.");
        byte[] data = new byte[32];
        Random random = new();
        random.NextBytes(data);

        // Convert the byte array to a string
        string secretKey = Convert.ToBase64String(data);

        return secretKey;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(secret);

        List<Claim> claims = [
            new Claim("UserId", user.Id.ToString()),
            new Claim("IsSuper", (user.IsSuper ?? false).ToString()),
            new Claim("PasswordChangeDate", user.PasswordChangeDate.ToString())
        ];

        SecurityTokenDescriptor tokenDescriptor = new() {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(jwtToken);
    }

    public bool ValidateJwtToken(string jwt)
    {
        try 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);

            TokenValidationParameters validationParameters = new() {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
            JwtSecurityToken validatedJwt = (JwtSecurityToken)validatedToken;

            int userId = int.Parse(validatedJwt.Claims.First(claim => claim.Type == "UserId").Value);
            DateTime PasswordChangeDate = DateTime.Parse(validatedJwt.Claims.First(claim => claim.Type == "passwordChangeDate").Value);

            User? user = _userRepo.GetUserById(userId);
            if (user == null || PasswordChangeDate < user.PasswordChangeDate)
            {
                return false;
            }
            else
            {
                //Other checks in the future
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}