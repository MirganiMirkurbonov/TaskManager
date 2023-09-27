using Domain.Schemas.Auth;
using Domain.Schemas.Project;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Schemas.Project.Task;

namespace Persistence.EntityContext;

public partial class EntityContext
{
    #region Auth

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    #endregion

    #region Project
    
    public virtual DbSet<Task> Tasks { get; set; }

    #endregion
}