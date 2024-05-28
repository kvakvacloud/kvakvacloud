using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Directories.Requests;

public class UpdateDirectoryRequest
{
    [Required]
    public string Location { get; set; } = null!;
    public string? NewName { get; set; }
}