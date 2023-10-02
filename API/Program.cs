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
      
        
        builder.Services
            .AddSwaggerConfigs()
            .AddDomain()
            .AddApplication(builder.Configuration)
            .AddPersistence();
        
        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
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