using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserDataService.Models.Privacy.Requests;

namespace UserDataService.Controllers;

[ApiController]
[Route("UserData/[controller]")]
public class PrivacyController(): ControllerBase
{
    /// <summary>
    /// Получить настройки приватности пользователя.
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    /// <response code="404"></response>
    [Route("get")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult GetSettings(GetPrivacySettingsRequest model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Изменить настройки приватности пользователя.
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    [Route("update")]
    [HttpPatch]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult UpdateSettings(UpdatePrivacySettingsRequest model)
    {
        throw new NotImplementedException();
    }
}