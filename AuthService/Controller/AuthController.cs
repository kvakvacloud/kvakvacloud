using AuthService.Model;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthService.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {

    private readonly IAccountService _accservice;

    public AuthController(IAccountService accountService)
    {
        _accservice = accountService;
    }

    [Route("register")]
    [HttpPost]
    public IActionResult Register([FromQuery] AccountRegisterModel model)
    {
        var result = _accservice.Register(model.Email);

        return StatusCode(result.Code, result.Payload);
    }   

    [Route("validateRegCode")]
    [HttpGet]
    public IActionResult ValidateRegCode([FromQuery] AccountRegCodeModel model)
    {
        var result = _accservice.ValidateRegCode(model.Code);

        return StatusCode(result.Code, result.Payload);
    }

    [Route("activate")]
    [HttpPost]
    public IActionResult Activate([FromQuery] AccountRegFormModel model)
    {
        var result = _accservice.Activate(model);

        return StatusCode(result.Code, result.Payload);
    }

    [Route("reset")]
    [HttpPost]
    public IActionResult Reset()
    {
        return new StatusCodeResult(501);
    }


    [Route("login")]
    [HttpPost]
    public IActionResult Login(AccountLoginModel model)
    {
        var result = _accservice.Login(model.Username, model.Password);

        return StatusCode(result.Code, result.Payload);
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