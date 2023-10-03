using Microsoft.Extensions.DependencyInjection;
using Persistence.Common;
using Persistence.DbContext;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection service)
    {
        service.AddDbContext<EntityContext>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return service;
    }
}