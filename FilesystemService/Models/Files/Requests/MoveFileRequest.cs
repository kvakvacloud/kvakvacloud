using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Files.Requests;

public class MoveFileRequest
{
    [Required]
    public string Location { get; set; } = null!;
    [Required]
    public string NewLocation { get; set; } = null!;
    public bool Overwrite { get; set; } = false;
    public bool Recursive { get; set; } = false;
}