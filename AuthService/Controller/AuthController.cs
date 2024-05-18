using AuthService.Model;
using AuthService.Responses;
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

    /// <summary>
    /// Запросить регистрацию по данному Email.
    /// </summary>
    /// <response code="200">Код отправлен на почту, если она не зарегистрирована.</response>
    [Route("register")]
    [HttpPost]
    public IActionResult Register([FromQuery] AccountRegisterModel model)
    {
        var result = _accservice.Register(model.Email);

        return StatusCode(result.Code, result.Payload);
    }   

    /// <summary>
    /// Проверить, что код регистрации валиден.
    /// </summary>
    /// <response code="200">Код валиден.</response>
    /// <response code="404">Код не существует или его срок действия истек.</response>
    [Route("validateRegCode")]
    [HttpGet]
    public IActionResult ValidateRegCode([FromQuery] AccountRegCodeModel model)
    {
        var result = _accservice.ValidateRegCode(model.Code);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Предоставить код активации аккаунта, заполнить форму и завершить регистрацию
    /// </summary>
    /// <response code="200">Регистрация успешна.</response>
    /// <response code="401">Действие кода истекло или код не существует.</response>
    [Route("activate")]
    [HttpPost]
    public IActionResult Activate([FromQuery] AccountRegFormModel model)
    {
        var result = _accservice.Activate(model);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Запрос на сброс пароля для аккаунта с указанной почтой. 
    /// </summary>
    /// <response code="200">Запрос отправлен на почту, если она зарегистрирована.</response>
    [Route("forgotPassword")]
    [HttpPost]
    public IActionResult ForgotPassword(AccountForgotPasswordModel model)
    {
        var result = _accservice.RequestPasswordReset(model.Email);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Сброс пароля аккаунта при помощи кода восстановления.
    /// </summary>
    /// <response code="200">Пароль успешно изменен.</response>
    [Route("resetPassword")]
    [HttpPost]
    public IActionResult ResetPassword(AccountPasswordResetModel model)
    {
        var result = _accservice.ResetPassword(model);

        return new StatusCodeResult(501);
    }

    /// <summary>
    /// Вход в аккаунт с использованием username и пароля.
    /// </summary>
    /// <response code="200">Получен токен</response>
    [ProducesResponseType(typeof(TokenResponse), (int)HttpStatusCode.OK)]
    [Route("login")]
    [HttpPost]
    public IActionResult Login(AccountLoginModel model)
    {
        var result = _accservice.Login(model.Username, model.Password);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Изменить пароль аккаунта.
    /// </summary>
    /// <response code="200">Получен токен</response>
    /// <response code="401">Неверный старый пароль или неверный токен</response>
    [Route("changePassword")]
    [HttpPut]
    [ProducesResponseType(typeof(TokenResponse), (int)HttpStatusCode.OK)]
    public IActionResult ChangePassword(AccountChangePasswordModel model)
    {
        var result = _accservice.ChangePassword(model);

        return StatusCode(result.Code, result.Payload);
    }

    // [Route("refreshToken")]
    // [HttpPost]
    // public IActionResult RefreshToken()
    // {
        // return new StatusCodeResult(501);
    // }
}