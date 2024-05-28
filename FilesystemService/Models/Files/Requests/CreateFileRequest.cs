using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Files.Requests;

public class CreateFileRequest
{
    [Required]
    public string Location { get; set; } = null!;
    public string Content { get;set; } = null!;
    public bool Recursive { get; set; } = false;
    public bool Overwrite { get; set; } = false;
}