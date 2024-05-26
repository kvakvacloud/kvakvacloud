using AuthService.Models.Requests;
using AuthService.Models.Responses;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAccountService accService, IMicroserviceAuthService mAuthService) : ControllerBase {

    private readonly IAccountService _accService = accService;
    private readonly IMicroserviceAuthService _mAuthService = mAuthService;

    /// <summary>
    /// Запросить регистрацию по данному Email.
    /// </summary>
    /// <response code="200">Код отправлен на почту, если она не зарегистрирована.</response>
    [Route("register")]
    [HttpPost]
    public IActionResult Register([FromBody] AccountRegisterRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.Register(model.Email ?? "");

        return StatusCode(result.Code, result.Payload);
    }   

    /// <summary>
    /// Проверить, что код регистрации валиден.
    /// </summary>
    /// <response code="200">Код валиден.</response>
    /// <response code="404">Код не существует или его срок действия истек.</response>
    [Route("validateRegCode")]
    [HttpGet]
    public IActionResult ValidateRegCode([FromQuery] AccountValidateRegCodeRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.ValidateRegCode(model);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Предоставить код активации аккаунта, заполнить форму и завершить регистрацию
    /// </summary>
    /// <response code="200">Регистрация успешна.</response>
    /// <response code="401">Действие кода истекло или код не существует.</response>
    [Route("activate")]
    [HttpPost]
    public IActionResult FinishRegistration([FromBody] AccountFinishRegistrationRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.FinishRegistration(model);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Запрос на сброс пароля для аккаунта с указанной почтой. 
    /// </summary>
    /// <response code="200">Запрос отправлен на почту, если она зарегистрирована.</response>
    [Route("forgotPassword")]
    [HttpPost]
    public IActionResult ForgotPassword([FromBody] AccountForgotPasswordRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.RequestPasswordReset(model.Email ?? "");

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Сброс пароля аккаунта при помощи кода восстановления.
    /// </summary>
    /// <response code="200">Пароль успешно изменен.</response>
    [Route("resetPassword")]
    [HttpPost]
    public IActionResult ResetPassword([FromBody] AccountPasswordResetRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.ResetPassword(model);

        return new StatusCodeResult(501);
    }

    /// <summary>
    /// Вход в аккаунт с использованием username и пароля.
    /// </summary>
    /// <response code="200">Получен токен</response>
    [ProducesResponseType(typeof(TokensResponse), (int)HttpStatusCode.OK)]
    [Route("login")]
    [HttpPost]
    public IActionResult Login([FromBody] AccountLoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.Login(model.Username ?? "", model.Password ?? "");

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Изменить пароль аккаунта.
    /// </summary>
    /// <response code="200">Получен токен</response>
    /// <response code="401">Неверный старый пароль или неверный токен</response>
    [Route("changePassword")]
    [HttpPut]
    [Authorize(Roles = "User", Policy = "Access")]
    [ProducesResponseType(typeof(TokensResponse), (int)HttpStatusCode.OK)]
    public IActionResult ChangePassword([FromBody] AccountChangePasswordRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.ChangePassword(model, User);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Запрос refresh и access токенов.
    /// </summary>
    /// <response code="200">Получены токены</response>
    [Route("refreshToken")]
    [HttpPost]
    [Authorize(Roles = "User", Policy = "Refresh")]
    [ProducesResponseType(typeof(TokensResponse), (int)HttpStatusCode.OK)]
    public IActionResult RefreshToken()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _accService.RefreshToken(User);

        return StatusCode(result.Code, result.Payload);
    }

    /// <summary>
    /// Запросить токен сервиса.
    /// </summary>
    /// <remarks>
    /// Используется для микросервисной аутентификации.
    /// </remarks>
    /// <response code="200">Успешно</response>
    [Route("serviceToken")]
    [HttpPost]
    [ProducesResponseType(typeof(ServiceTokenResponse), (int)HttpStatusCode.OK)]
    public IActionResult ServiceToken(RequestServiceTokenRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _mAuthService.ServiceToken(model);

        return StatusCode(result.Code, result.Payload);
    }
}