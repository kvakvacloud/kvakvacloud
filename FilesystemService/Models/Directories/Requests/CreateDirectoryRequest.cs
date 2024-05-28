using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Directories.Requests;

public class CreateDirectoryRequest
{
    [Required]
    public string Location { get; set; } = null!;
    public bool Recursive { get; set; } = false;
}