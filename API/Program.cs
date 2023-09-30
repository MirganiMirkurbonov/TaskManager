using System.Reflection;
using System.Runtime.Intrinsics.X86;
using Domain.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NLog;

var logger = LogManager
    .Setup()
    .LoadConfigurationFromFile("nlog.config")
    .GetCurrentClassLogger();

logger.Info("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        
        builder.Services
            .AddDomain();
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    var app = builder.Build();
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }
}
catch (Exception exception)
{
    logger.Fatal(exception, "Stopped program because of exception!");
    throw;
}
finally
{
    LogManager.Shutdown();
}