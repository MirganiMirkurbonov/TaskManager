using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence.EntityContext;

public partial class EntityContext : DbContext
{
    private readonly string _connectionString;

    public EntityContext(IOptions<ConnectionStringOptions> options)
    {
        _connectionString = options.Value.Database;
    }
}