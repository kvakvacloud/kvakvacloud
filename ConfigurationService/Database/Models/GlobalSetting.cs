using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Database.Models;

[Index(nameof(Section), nameof(Name), IsUnique = true)]
public class GlobalSetting {
    [Key]
    public int Id {get;set;}
    [Required] [Column(TypeName = "VARCHAR(50)")]
    public string? Section {get;set;}
    [Required] [Column(TypeName = "VARCHAR(50)")]
    public string? Name {get;set;}
    [Column(TypeName = "VARCHAR(1000)")]
    public string? Value {get;set;}
    [Required] [Column(TypeName = "VARCHAR(50)")]
    public byte Type {get;set;}
};