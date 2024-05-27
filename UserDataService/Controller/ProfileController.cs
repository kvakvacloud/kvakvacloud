using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserDataService.Models.Requests;

namespace UserDataService.Controllers;

[ApiController]
[Route("UserData/[controller]")]
public class ProfileController(): ControllerBase
{
    /// <summary>
    /// Получить профиль пользователя.
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    /// <response code="404"></response>
    [Route("get")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult GetProfile([FromQuery] UserDataGetProfileRequest model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Изменить данные профиля
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    [Route("update")]
    [HttpPatch]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult UpdateProfile([FromQuery] UserDataUpdateProfileRequest model)
    {
        throw new NotImplementedException();
    }
}