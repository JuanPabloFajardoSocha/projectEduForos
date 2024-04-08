using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eduForos.Application.Common.Interfaces.Services.Authentication;
using eduForos.Application.Common.Interfaces.Services.Others;
using eduForos.Infrastructure.Settings.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace eduForos.Infrastructure.Services.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenService(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid idUser, string institutionalEmail, string userType)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, idUser.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, institutionalEmail),
            new Claim(JwtRegisteredClaimNames.Typ, userType),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes).DateTime,
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }

    public JwtSecurityToken DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.ReadJwtToken(token);
        
    }







}
