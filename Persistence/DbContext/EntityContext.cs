using Domain.Options.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence.DbContext;

public partial class EntityContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly string _connectionString;
    
    public EntityContext(IOptions<ConnectionStringOptions> options)
    {
        _connectionString = options.Value.Database;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString,
            builder => { builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(2), null); });

}