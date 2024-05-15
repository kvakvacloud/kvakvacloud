using AuthService.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthService.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {

    [Route("register")]
    [HttpPost]
    public HttpResponseMessage Register(HttpContext context)
    {
        return new HttpResponseMessage(HttpStatusCode.NotImplemented);
    }

    [Route("login")]
    [HttpPost]
    public HttpResponseMessage Login()
    {
        return new HttpResponseMessage(HttpStatusCode.NotImplemented);
    }

    [Route("forgotPassword")]
    [HttpPost]
    public HttpResponseMessage ForgotPassword()
    {
        return new HttpResponseMessage(HttpStatusCode.NotImplemented);
    }

    [Route("changePassword")]
    [HttpPut]
    public HttpResponseMessage ChangePassword()
    {
        return new HttpResponseMessage(HttpStatusCode.NotImplemented);
    }

    [Route("refreshToken")]
    [HttpPost]
    public HttpResponseMessage RefreshToken()
    {
        return new HttpResponseMessage(HttpStatusCode.NotImplemented);
    }
}