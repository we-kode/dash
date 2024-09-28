using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Persistence.Models;

[Table("informations")]
public class Information
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
    public string? Text { get; set; }
    public string? Image { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ExpiredDate { get; set; }
    public bool IsFocused { get; set; }
    public string? Config { get; set; } = "{}";
}
