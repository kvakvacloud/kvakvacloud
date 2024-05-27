using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserDataService.Models.Notifications.Requests;

namespace UserDataService.Controllers;

[ApiController]
[Route("UserData/[controller]")]
public class NotificationController(): ControllerBase
{
    /// <summary>
    /// Получить список уведомлений.
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    /// <response code="404"></response>
    [Route("get")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult GetAll([FromQuery] GetNotificationsRequest model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отметить уведомление как прочитанное.
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    [Route("read")]
    [HttpPut]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult Read([FromQuery] MarkNotificationAsReadRequest model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удалить уведомление.
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    [Route("delete")]
    [HttpDelete]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult Delete([FromQuery] DeleteNotificationRequest model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Очистить все уведомления.
    /// </summary>
    /// <response code="200"></response>
    /// <response code="403"></response>
    [Route("clearAll")]
    [HttpDelete]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult ClearAll([FromQuery] ClearNotificationsRequest model)
    {
        throw new NotImplementedException();
    }
}