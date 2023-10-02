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
        
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = jwtConfigOptions.Issuer,
                ValidAudience = jwtConfigOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.Default.GetBytes(jwtConfigOptions.SecretKey))
            });

        return service;
    }
}