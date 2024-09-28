using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Persistence.Models;

[Table("displays")]
public class Display
{
    [Key]
    public Guid DisplayId { get; set; }
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public string? Description { get; set; }

    [Required]
    public int Rows { get; set; }

    [Required]
    public int Columns { get; set; }

    [Required]
    public Guid ShareId { get; set; }

    public ICollection<Element> Elements { get; set; } = [];
}
