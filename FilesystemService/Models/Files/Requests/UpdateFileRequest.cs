using System.ComponentModel.DataAnnotations;

namespace FilesystemService.Models.Files.Requests;

public class UpdateFileRequest
{
    [Required]
    public string Location { get; set; } = null!;
    public string? NewName { get; set; }
    public string? NewContent { get; set; }
    public UpdateMethod Method { get; set; } = UpdateMethod.Overwrite;

}