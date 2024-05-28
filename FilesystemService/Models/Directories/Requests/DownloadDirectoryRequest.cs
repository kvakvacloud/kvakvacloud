using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Directories.Requests;

public class DownloadDirectoryRequest
{
    [Required]
    public string Location { get; set; } = null!;
}