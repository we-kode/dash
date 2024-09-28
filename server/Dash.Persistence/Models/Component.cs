using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Persistence.Models;

[Table("components")]
public class Component
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Identifier { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Image { get; set; }

    [Required]
    public string Config { get; set; } = string.Empty;

    public ICollection<Element> Elements { get; } = [];
}
