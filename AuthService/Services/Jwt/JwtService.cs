using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Database.Models;
using AuthService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services.Jwt;

public class JwtService(IUserRepository userRepo) : IJwtService
{
    private readonly IUserRepository _userRepo = userRepo;
    private readonly string _secretKey = Environment.GetEnvironmentVariable("AUTH_JWT_SECRET") ?? "";
    private readonly string _issuer = Environment.GetEnvironmentVariable("AUTH_JWT_ISSUER") ?? "kvakvacloud";
    private readonly string _audience = Environment.GetEnvironmentVariable("AUTH_JWT_AUDIENCE") ?? "kvakvacloud";

    public string GenerateAccessToken(User user)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            { 
                new Claim("Type", "access"),
                new Claim("Username", user.Username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _issuer,
            Audience = _audience,
            Subject = new ClaimsIdentity(new[] 
            {
                new Claim("Type", "refresh"),
                new Claim("Username", user.Username),
                new Claim("PasswordChangeDate", user.PasswordChangeDate.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateMicroserviceToken(string microserviceId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _issuer,
            Audience = _audience,

            Subject = new ClaimsIdentity(new[] 
            {
                new Claim("Type", "microservice"),
                new Claim("Microservice", microserviceId)
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateAccessToken(string? token, out string? username)
    {
        try
        {
            if (token == null)
            {
                username = null;
                return false;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secretKey);
    
            TokenValidationParameters validationParameters = new() {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
    
            // Валидация токена
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            JwtSecurityToken validatedJwt = (JwtSecurityToken)validatedToken;

            username = validatedJwt.Claims.First(claim => claim.Type == "Username").Value;
            var passwordChangeDate = DateTime.Parse(validatedJwt.Claims.First(claim => claim.Type == "PasswordChangeDate").Value);

            // Проверка типа токена
            if (validatedJwt.Claims.First(claim => claim.Type == "Type").Value != "active")
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            username = null;
            return false;
        }  
    }

    public bool ValidateRefreshToken(string? token, out string? username)
    {
        try
        {
            if (token == null)
            {
                username = null;
                return false;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secretKey);
    
            TokenValidationParameters validationParameters = new() {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
    
            // Валидация токена
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            JwtSecurityToken validatedJwt = (JwtSecurityToken)validatedToken;

            username = validatedJwt.Claims.First(claim => claim.Type == "Username").Value;
            var passwordChangeDate = DateTime.Parse(validatedJwt.Claims.First(claim => claim.Type == "PasswordChangeDate").Value);

            // Проверка типа токена
            if (validatedJwt.Claims.First(claim => claim.Type == "Type").Value != "refresh")
            {
                username=null;
                return false;
            }

            // Проверка, что дата изменения пароля совпадает с фактической
            if ((_userRepo.GetUserByUsername(username) ?? new User()).PasswordChangeDate != passwordChangeDate)
            {
                username=null;
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            username=null;
            return false;
        }
    }

    public bool ValidateMicroserviceToken(string? token, out string? microservice)
    {
        try
        {
            if (token == null)
            {
                microservice = null;
                return false;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secretKey);
    
            TokenValidationParameters validationParameters = new() {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
    
            // Валидация токена
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            JwtSecurityToken validatedJwt = (JwtSecurityToken)validatedToken;

            // Проверка типа токена
            if (validatedJwt.Claims.First(claim => claim.Type == "Type").Value != "microservice")
            {
                microservice=null;
                return false;
            }

            microservice = validatedJwt.Claims.First(claim => claim.Type == "microservice").Value;
            return true;
            
        }
        catch (Exception)
        {
            microservice = null;
            return false;
        }
    }
}