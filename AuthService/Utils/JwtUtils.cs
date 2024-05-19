using AuthService.Database.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Repository;

namespace AuthService.Utils;

public class JwtUtils : IJwtUtils {

    private readonly IUserRepository _userRepo;

    private readonly string secret = Environment.GetEnvironmentVariable("AUTH_JWT_SECRET") ?? GenerateTempSecret();
    private readonly int expireMinutesAccess = Int32.Parse(Environment.GetEnvironmentVariable("AUTH_JWT_EXPIRE_ACCESS") ?? "5");
    private readonly int expireMinutesRefresh = Int32.Parse(Environment.GetEnvironmentVariable("AUTH_JWT_EXPIRE_REFRESH") ?? "10");
    private readonly int expireMinutesService = Int32.Parse(Environment.GetEnvironmentVariable("AUTH_JWT_EXPIRE_SERVICE") ?? "5");

    public JwtUtils(IUserRepository userRepo)
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

    public JwtSecurityToken StringToJwtToken(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        
        return token;
    }

    public IEnumerable<Claim> JwtTokenClaims(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(jwt).Claims;
    }

    public string GenerateJwtToken(User user, string type)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(secret);

        List<Claim> claims = [
            new Claim("UserId", user.Id.ToString()),
            new Claim("Username", user.Username ?? ""),
            new Claim("IsSuper", (user.IsSuper ?? false).ToString()),
            new Claim("PasswordChangeDate", user.PasswordChangeDate.ToString()),
            new Claim ("Type", type)
        ];

        SecurityTokenDescriptor tokenDescriptor = new() {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(type == "refresh" ? expireMinutesRefresh : expireMinutesAccess),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(jwtToken);
    }

    public string GenerateServiceJwtToken(string service, string ip)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(secret);

        List<Claim> claims = [
            new Claim("Service", service),
            new Claim("IP", ip)
        ];

        SecurityTokenDescriptor tokenDescriptor = new() {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutesService),
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

            //Lighter check for access tokens
            if (validatedJwt.Claims.First(claim => claim.Type == "Type").Value == "access")
            {
                return true;
            }

            //Lighter check for service tokens
            if (validatedJwt.Claims.First(claim => claim.Type == "Type").Value == "service")
            {
                return true;
            }

            int userId = int.Parse(validatedJwt.Claims.First(claim => claim.Type == "UserId").Value);
            DateTime PasswordChangeDate = DateTime.Parse(validatedJwt.Claims.First(claim => claim.Type == "PasswordChangeDate").Value);

            User? user = _userRepo.GetUserById(userId);
            if (user == null || PasswordChangeDate.AddSeconds(1) < user.PasswordChangeDate)
            {
                return false;
            }
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}