using System.ComponentModel.DataAnnotations;
using FilesystemService.Enums.Sharing;

namespace FilesystemService.Database.Models;

public class DirectoryShare
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string OwnerUsername { get; set; } = null!;
    [Required]
    public string Location { get; set; } = null!;
    public ShareType Type { get; set; } = ShareType.ByLink;
    public ShareSecurity Security { get; set; } = ShareSecurity.None;
    public ShareAvailability Availability { get; set; } = ShareAvailability.Unlimited;
    public string? ToUsers { get; set; }
    public string? ToCircles { get; set; } 
    public long MaxDownloadsCount { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public long DownloadsCount { get; set; }
    [MinLength(6)]
    public string? Password { get; set; }
}