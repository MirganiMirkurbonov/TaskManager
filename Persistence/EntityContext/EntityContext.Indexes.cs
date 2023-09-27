using Domain.Schemas.Auth;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Schemas.Project.Task;

namespace Persistence.EntityContext;

public partial class EntityContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Auth

        modelBuilder.Entity<AuthUser>()
            .HasIndex(_ => _.Username)
            .IsUnique();
        
        modelBuilder.Entity<AuthUser>()
            .HasIndex(_ => _.Email)
            .IsUnique();
        
        #endregion

        #region Task

        modelBuilder.Entity<Task>()
            .HasIndex(_ => new {_.AuthUserId, _.StartDate});
        
        #endregion
        
        base.OnModelCreating(modelBuilder);
    }
}