using API.Extensions;
using Application.Extensions;
using Domain.Extensions;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NLog;
using Persistence.Extensions;

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
        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services
            .AddApplication(builder.Configuration)
            .AddSwaggerConfigs()
            .AddDomain()
            .AddPersistence();
    }

    var app = builder.Build();
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        if (app.Services.GetService<IHttpContextAccessor>() != null)
            HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.UseAuthorization();

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