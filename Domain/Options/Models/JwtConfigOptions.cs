using System.Reflection.Metadata;

namespace Domain.Options.Models;

public class JwtConfigOptions
{
    public const string SectionName = "JwtOptions";
    public int ExpireMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    
}