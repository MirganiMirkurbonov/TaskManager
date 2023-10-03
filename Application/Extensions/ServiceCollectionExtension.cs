using System.Text;
using Application.Common;
using Application.Interfaces;
using Application.Services;
using Domain.Options.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(
        this IServiceCollection service,
        ConfigurationManager configurationManager)
    {
        service.AddAuth(configurationManager);
        service.AddScoped<IUser, UserRepository>();
        service.AddScoped<ITask, TaskRepository>();
        service.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return service;
    }

    private static IServiceCollection AddAuth(
        this IServiceCollection service,
        ConfigurationManager configurationManager)
    {
        #region

        var jwtConfigOptions = new JwtConfigOptions();
        configurationManager.Bind(JwtConfigOptions.SectionName, jwtConfigOptions);

        #endregion

        var keyByteArray = Encoding.UTF8.GetBytes(jwtConfigOptions.SecretKey);
        var signingKey = new SymmetricSecurityKey(keyByteArray);
        
        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => options.TokenValidationParameters =
                new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    
                    ValidIssuer = jwtConfigOptions.Issuer,
                    ValidAudience = jwtConfigOptions.Audience,
                    
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = signingKey
                });

        return service;
    }
}