using Domain.Models.Request;
using Domain.Options.SetupManager;
using Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
       services.AddScoped<IValidator<SignUpRequest>, SignUpRequestValidator>();
       services.AddScoped<IValidator<SignInRequest>, SignInRequestValidator>();
        
        services.ConfigureOptions<ConnectionStringOptionsSetupManager>();
        services.ConfigureOptions<JwtOptionsSetupManager>();
        return services;
    } 
}