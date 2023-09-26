using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Auth;

[Table("auth_permission", Schema = "auth")]
public class AuthPermission : Entity
{
    [Column("title"), MaxLength(100)]
    public string? Title { get; set; }

    [Column("keyword"), MaxLength(50)]
    public string Keyword { get; set; } = null!;
}