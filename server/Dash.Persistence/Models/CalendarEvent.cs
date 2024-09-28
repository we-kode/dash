using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Persistence.Models;

[Table("calendar_events")]
public class CalendarEvent
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string UID { get; set; } = "@dash.wekode";
    public string? Location { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string Class { get; set; } = "PUBLIC";

    [Required]
    public DateTime DtStart { get; set; }

    [Required]
    public DateTime DtEnd { get; set; }

    [Required]
    public DateTime DtStamp { get; set; }

    [Required]
    public Guid CalendarId { get; set; }
    public required Calendar Calendar { get; set; }
}
