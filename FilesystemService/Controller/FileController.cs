using System.Runtime.InteropServices;
using FilesystemService.Models.Files.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FilesService.Controllers;

[ApiController]
[Route("Filesystem/[controller]")]
public class FileController() : ControllerBase 
{
    /// <summary>
    /// Создать файлы пользователя.
    /// </summary>
    /// <response code="200"></response>
    [Route("create")]
    [HttpPost]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult CreateFiles(List<CreateFileRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Перезаписать или дополнить файлы пользователя.
    /// </summary>
    /// <response code="200"></response>
    [Route("update")]
    [HttpPut]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult UpdateFiles(List<UpdateFileRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Переместить файлы пользователя в другую локацию.
    /// </summary>
    /// <response code="200"></response>
    [Route("move")]
    [HttpPut]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult MoveFiles(List<MoveFileRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удалить файлы пользователя.
    /// </summary>
    /// <response code="200"></response>
    
    [Route("delete")]
    [HttpDelete]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult DeleteFiles(List<DeleteFileRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить информацию о файлах пользователя.
    /// </summary>
    /// <response code="200"></response>
    [Route("info")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult Info(List<GetFileRequest> model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Скачать файлы.
    /// </summary>
    /// <response code="200"></response>
    [Route("download")]
    [HttpGet]
    [Authorize(Roles = "User", Policy = "Access")]
    public IActionResult Download(List<GetFileRequest> model)
    {
        throw new NotImplementedException();
    }
}