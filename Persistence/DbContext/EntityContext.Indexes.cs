using Domain.Schemas.Auth;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Schemas.Project.Task;

namespace Persistence.DbContext;

public partial class EntityContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Auth

        modelBuilder.Entity<AuthUser>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<AuthUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        #endregion

        #region Task

        modelBuilder.Entity<Task>()
            .HasIndex(t => new { t.AuthUserId, t.StartDate });
        
        #endregion
        
        base.OnModelCreating(modelBuilder);
    }
}