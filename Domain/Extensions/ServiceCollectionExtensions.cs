using Domain.Options;
using Domain.Options.SetupManager;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.ConfigureOptions<ConnectionStringOptionsSetupManager>();
        return services;
    } 
}