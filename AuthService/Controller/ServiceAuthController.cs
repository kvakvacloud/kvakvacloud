using AuthService.Models.Requests;
using AuthService.Models.Responses;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceAuthController : ControllerBase {

    private readonly IMicroserviceAuthService _mauthService;

    public ServiceAuthController(IMicroserviceAuthService mauthSerivce)
    {
        _mauthService = mauthSerivce;
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
        var result = _mauthService.ServiceToken(model);

        return StatusCode(result.Code, result.Payload);
    }
}