using Domain.Schemas.Auth;
using Domain.Schemas.Project;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Schemas.Project.Task;

namespace Persistence.EntityContext;

public partial class EntityContext
{
    #region Auth

    public virtual DbSet<AuthUser> AuthUsers { get; set; }
    public virtual DbSet<AuthRole> AuthRoles { get; set; }
    public virtual DbSet<AuthPermission> AuthPermissions { get; set; }
    public virtual DbSet<AuthRolePermission> AuthRolePermissions { get; set; }

    #endregion

    #region Project

    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<TaskMembers> TaskMembers { get; set; }

    #endregion
}