using Domain.Extensions;
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
