using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Auth;

public class AuthUser : Entity
{
    [Column("first_name")]
    public string FirstName { get; set; } = null!;
    
    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("username")]
    public string Username { get; set; } = null!;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [Column("password_salt")]
    public string PasswordSalt { get; set; } = null!;
}
