using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eduForos.Application.Common.Interfaces.Services.Others;
using eduForos.Infrastructure.Settings.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace eduforos.Infrastructure.Settings.Others
{
    public class Jitsi : IJitsiService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;

        public Jitsi(JwtSettings jwtSettings, IConfiguration configuration)
        {
            _jwtSettings = jwtSettings;
            _configuration = configuration;
        }

        public string GenerateJitsiToken(string roomName, string userName, string institutionalEmail, string urlPhoto)
        {
            var appId = _configuration["Jitsi:AppId"];
            var appSecret = _configuration["Jitsi:AppSecret"];
            var domain = _configuration["Jitsi:Domain"];

#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(appSecret)),
                    SecurityAlgorithms.HmacSha256);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            var claims = new[]
            {
                new Claim("context", JsonConvert.SerializeObject(new
                {
                    user = new
                    {
                        avatar = urlPhoto,
                        name = userName,
                        email = institutionalEmail
                    },
                    room = roomName
                })),
                new Claim("aud", _jwtSettings.Audience),
                new Claim("iss", _jwtSettings.Issuer),
                new Claim("sub", "meet.eduforos.com"),
                new Claim("exp", DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes).ToUnixTimeSeconds().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: appId,
                audience: domain,
                expires: DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes).DateTime,
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

    }
}
