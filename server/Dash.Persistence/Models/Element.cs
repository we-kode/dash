using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Persistence.Models;

[Table("elements")]
public class Element
{
    [Key]
    public Guid ElementId {  get; set; }

    [Required]
    public string Config { get; set; } = string.Empty;
    public string? Content { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
    public int Cols {  get; set; }
    public int Rows { get; set; }

    [Required]
    public Guid ComponentId { get; set; }
    public required Component Component { get; set; }

    [Required]
    public Guid DisplayId { get; set; }
    public required Display Display { get; set; }
}
