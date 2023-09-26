using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Auth;

[Table("auth_role", Schema = "auth")]
public class AuthRole : Entity
{
    [Column("title"), MaxLength(200)]
    public string? Title { get; set; }
    
    [Column("keyword")]
    public string Keyword { get; set; } = null!;
}

// You can replace the attributes to fluent API, It would we more powerful and clean code/architecture