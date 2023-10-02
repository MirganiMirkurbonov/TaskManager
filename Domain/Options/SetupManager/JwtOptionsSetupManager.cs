using Domain.Options.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Domain.Options.SetupManager;

public class JwtOptionsSetupManager: IConfigureOptions<JwtConfigOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetupManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(JwtConfigOptions options)
    {
        _configuration
            .GetSection(JwtConfigOptions.SectionName)
            .Bind(options);
    }
}