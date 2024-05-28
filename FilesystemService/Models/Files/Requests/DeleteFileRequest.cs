using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Files.Requests;

public class DeleteFileRequest
{
    [Required]
    public string Location { get; set; } = null!;
}