using System.Reflection;
using Domain.Options;
using Domain.Options.SetupManager;
using Domain.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<SignUpRequestValidator>();
        
        services.ConfigureOptions<ConnectionStringOptionsSetupManager>();
        return services;
    } 
}