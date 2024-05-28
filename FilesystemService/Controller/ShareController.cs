using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FilesService.Controllers;

[ApiController]
[Route("Filesystem/[controller]")]
public class ShareController() : ControllerBase 
{
    /// <summary>
    /// Поделиться доступом к файлу или директории.
    /// </summary>
    /// <response code="200"></response>
    [Route("create")]
    [HttpPost]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult CreateShare(List<int> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Изменить настройки шеринга файла или директории.
    /// </summary>
    /// <response code="200"></response>
    [Route("update")]
    [HttpPut]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult UpdateShare(List<int> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Прекратить шеринг файла или директории..
    /// </summary>
    /// <response code="200"></response>
    [Route("delete")]
    [HttpDelete]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult DeleteShare(List<int> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить информацию о шеринге файла или директории.
    /// </summary>
    /// <response code="200"></response>
    [Route("info")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult GetShareInfo(List<int> model)
    {
        throw new NotImplementedException();
    }
}