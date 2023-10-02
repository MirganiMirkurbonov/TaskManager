using Domain.Schemas.Auth;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Schemas.Project.Task;

namespace Persistence.DbContext;

public partial class EntityContext
{
    #region Auth

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    #endregion

    #region Task
    
    public virtual DbSet<Task> Tasks { get; set; }

    #endregion
}