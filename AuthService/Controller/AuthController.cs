using AuthService.Model;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthService.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {

    private readonly AccountService _accservice;

    public AuthController(AccountService accountService)
    {
        _accservice = accountService;
    }

    [Route("register")]
    [HttpPost]
    public IActionResult Register([FromBody] AccountRegisterModel model)
    {
        if (!ModelState.IsValid || model.Email == null || model.Password == null)
        {
            return BadRequest(ModelState);
        }

        var result = _accservice.Register(model.Email, model.Password);

        return new StatusCodeResult(501);
    }   

    [Route("login")]
    [HttpPost]
    public IActionResult Login()
    {
        return new StatusCodeResult(501);
    }

    [Route("forgotPassword")]
    [HttpPost]
    public IActionResult ForgotPassword()
    {
        return new StatusCodeResult(501);
    }

    [Route("changePassword")]
    [HttpPut]
    public IActionResult ChangePassword()
    {
        return new StatusCodeResult(501);
    }

    [Route("refreshToken")]
    [HttpPost]
    public IActionResult RefreshToken()
    {
        return new StatusCodeResult(501);
    }
}