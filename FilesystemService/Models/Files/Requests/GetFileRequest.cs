using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Files.Requests;

public class GetFileRequest
{
    [Required]
    public string Location { get; set; } = null!;
}