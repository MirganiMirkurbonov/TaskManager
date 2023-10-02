using Domain.Options.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Domain.Options.SetupManager;

public class ConnectionStringOptionsSetupManager : IConfigureOptions<ConnectionStringOptions>
{
    private const string SectionName = "ConnectionStrings";
    private readonly IConfiguration _configuration;

    public ConnectionStringOptionsSetupManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(ConnectionStringOptions options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}