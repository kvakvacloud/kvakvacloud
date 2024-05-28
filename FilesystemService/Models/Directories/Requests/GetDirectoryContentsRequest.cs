using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Directories.Requests;

public class GetDirectoryContentsRequest
{
    [Required]
    public string Location { get; set; } = null!;
}