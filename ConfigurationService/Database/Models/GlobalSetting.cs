using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ConfigurationService.Database.Models;

public class GlobalSetting {

    [NotNull] [Column(TypeName = "VARCHAR(50)")]
    public string? Section {get;set;}
    [NotNull] [Column(TypeName = "VARCHAR(50)")]
    public string? Name {get;set;}
    [Column(TypeName = "VARCHAR(1000)")]
    public string? Value {get;set;}
    [NotNull] [Column(TypeName = "VARCHAR(50)")]
    public byte Type {get;set;}
};