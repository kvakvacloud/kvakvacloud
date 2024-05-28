using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Directories.Requests;

public class GetDirectoryInfoRequest
{
    [Required]
    public string Location { get; set; } = null!;
}