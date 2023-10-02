using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models.Inner;
using Domain.Options.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common;

internal class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtConfigOptions _options;

    public JwtTokenGenerator(IOptions<JwtConfigOptions> options)
    {
        _options = options.Value;
    }

    public JwtGeneratedResponse GenerateTokenAsync(
        string firstName,
        string? lastName,
        string userId)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName ?? string.Empty)
        };

        var tokenExpireDate = DateTime.Now.AddMinutes(_options.ExpireMinutes);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: tokenExpireDate,
            issuer: _options.Issuer,
            audience: _options.Audience);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new JwtGeneratedResponse(token, tokenExpireDate);
    }
}