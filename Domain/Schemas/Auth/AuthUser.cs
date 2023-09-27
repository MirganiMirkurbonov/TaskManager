using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Auth;

[Table("auth_user", Schema = "auth")]
public class AuthUser : Entity
{
    [Column("first_name"), MaxLength(50)]
    public string FirstName { get; set; } = null!;
    
    [Column("last_name"), MaxLength(50)]
    public string? LastName { get; set; }

    [Column("username"), MaxLength(30)]
    public string Username { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [Column("password_salt")]
    public string PasswordSalt { get; set; } = null!;
}