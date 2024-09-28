using System;
using System.ComponentModel.DataAnnotations;

namespace Dash.Server.Contracts;

public class CalendarRequest
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Color { get; set; }
}

public class CalendarEventRequest
{
    public string? Location { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }

    [Required]
    public DateTime DtStart { get; set; }

    [Required]
    public DateTime DtEnd { get; set; }

    [Required]
    public Guid CalendarId { get; set; }
}
