using System.ComponentModel.DataAnnotations;
using System;

namespace Dash.Server.Contracts;

public class ElementRequest
{
    [Required]
    public string Config { get; set; } = string.Empty;
    public string? Content { get; set; }
    public DateTime? ExpireDate { get; set; }
    [Required]
    public Guid ComponentId { get; set; }
    [Required]
    public Guid DisplayId { get; set; }

    public int X {  get; set; }
    public int Y { get; set; }
    public int Cols { get; set; }
    public int Rows { get; set; }
}
