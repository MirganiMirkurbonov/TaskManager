using Domain.Schemas.Auth;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityContext;

public partial class EntityContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Auth

        modelBuilder.Entity<AuthUser>()
            .HasIndex(_ => _.Username)
            .IsUnique();
        
        modelBuilder.Entity<AuthRole>()
            .HasIndex(_ => _.Keyword)
            .IsUnique();
        
        modelBuilder.Entity<AuthPermission>()
            .HasIndex(_ => _.Keyword)
            .IsUnique();

        #endregion
        
        base.OnModelCreating(modelBuilder);
    }
}