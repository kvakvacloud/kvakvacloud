using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Directories.Requests;

public class DeleteDirectoryRequest
{
    [Required]
    public string Location { get; set; } = null!;
    public bool WithContents { get; set; } = false;
}