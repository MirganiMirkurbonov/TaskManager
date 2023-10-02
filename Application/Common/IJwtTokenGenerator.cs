using Domain.Models.Inner;

namespace Application.Common;

public interface IJwtTokenGenerator
{
    JwtGeneratedResponse GenerateTokenAsync(string firstName, string? lastName, string id);
}