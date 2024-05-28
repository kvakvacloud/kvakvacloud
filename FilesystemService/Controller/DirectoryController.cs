using FilesystemService.Models.Directories.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilesystemService.Controller;

[ApiController]
[Route("Filesystem/[controller]")]
public class DirectoryController() : ControllerBase 
{
    /// <summary>
    /// Создать директории пользователя.
    /// </summary>
    /// <response code="200"></response>
    [Route("create")]
    [HttpPost]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult CreateDirectories(List<CreateDirectoryRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Обновить директории пользователя.
    /// </summary>
    /// <response code="200"></response>
    [Route("update")]
    [HttpPut]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult UpdateDirectories(List<UpdateDirectoryRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удалить директории пользователя.
    /// </summary>
    /// <response code="200"></response>
    [Route("delete")]
    [HttpDelete]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult DeleteDirectories(List<DeleteDirectoryRequest> model)
    {
        throw new NotImplementedException();
    }

    [Route("move")]
    [HttpPut]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult MoveDirectories(List<MoveDirectoryRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить содержимое директории.
    /// </summary>
    /// <response code="200"></response>
    [Route("getContents")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult GetContents(List<GetDirectoryContentsRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить информацию о директории.
    /// </summary>
    /// <response code="200"></response>
    [Route("getInfo")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult GetDirectoryInfo(List<GetDirectoryInfoRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Скачать директорию как архив.
    /// </summary>
    /// <response code="200"></response>
    [Route("downloadDirectory")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult DownloadDirectory(List<DownloadDirectoryRequest> model)
    {
        throw new NotImplementedException();
    }
}