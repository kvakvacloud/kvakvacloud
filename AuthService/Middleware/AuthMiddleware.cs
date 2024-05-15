using AuthService.Database;
using AuthService.Database.Models;
using AuthService.Responses;
using AuthService.Utils;
using Newtonsoft.Json;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var username = context.Request.Query["username"];
        var password = context.Request.Query["password"].ToString();

        using ApplicationContext ctx = new();

        User? user = ctx.Users.First(u => u.Username == username);

        if (user == null || !BcryptUtils.VerifyPassword(password, user.Password))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Wrong username or password");
            return;
        }

        var token = JwtUtils.GenerateJwtToken(user);
        TokenResponse response = new () {token=token};

        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsJsonAsync(JsonConvert.SerializeObject(response));

        await _next(context);
    }
}