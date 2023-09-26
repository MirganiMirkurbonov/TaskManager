using System.ComponentModel.DataAnnotations.Schema;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Auth;

[Table("auth_role_permission", Schema = "auth")]
public class AuthRolePermission : Entity
{
    [Column("auth_role_id")]
    public long AuthRoleId { get; set; }

    [ForeignKey(nameof(AuthRoleId))]
    public virtual AuthRole AuthRole { get; set; }


    [Column("auth_permission_id")]
    public long AuthPermissionId { get; set; }

    [ForeignKey(nameof(AuthPermissionId))]
    public virtual AuthPermission AuthPermission { get; set; }
}