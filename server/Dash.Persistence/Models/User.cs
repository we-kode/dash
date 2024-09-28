using CryptoHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Persistence.Models;

[Table("users")]
public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string UserPassword { get; private set; } = string.Empty;

    [NotMapped]
    [Required]
    public string ClearTextPassword { set { UserPassword = Crypto.HashPassword(value); } }

    public bool Verify(string username, string password) => username == UserName && Crypto.VerifyHashedPassword(UserPassword, password);
}
