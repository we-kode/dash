using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Persistence.Models;

[Table("calendars")]
public class Calendar
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string ProdId { get; set; } = "wekode.dash@";

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string HexColor { get; set; } = string.Empty;

    [Required]
    public string Version { get; set; } = "2.0";

    public ICollection<CalendarEvent> Events { get; set; } = [];
}
