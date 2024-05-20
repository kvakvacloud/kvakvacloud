using System.Net;
using ConfigurationService.Model;
using ConfigurationService.Responses;
using ConfigurationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Controller;

[ApiController]
[Route("[controller]")]
public class GlobalSettingsController : ControllerBase {

    private readonly IGlobalSettingsService _gsService;

    public GlobalSettingsController(IGlobalSettingsService gsService)
    {
        _gsService = gsService;
    }

    /// <summary>
    /// Запросить токен сервиса.
    /// </summary>
    /// <remarks>
    /// Используется для микросервисной аутентификации.
    /// </remarks>
    /// <response code="200">Успешно</response>
    [Route("get")]
    [HttpPost]
    [ProducesResponseType(typeof(GlobalSettingResponse), (int)HttpStatusCode.OK)]
    public IActionResult Get(GetGlobalSettingModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _gsService.Get(model);

        return StatusCode(result.Code, result.Payload);
    }

}