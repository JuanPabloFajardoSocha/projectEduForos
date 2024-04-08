using System.IdentityModel.Tokens.Jwt;

namespace eduForos.Application.Common.Interfaces.Services.Authentication;


public interface IJwtTokenService
{
    string GenerateToken(Guid idUser, string institutionalEmail, string userType);
    JwtSecurityToken DecodeToken(string token);
    
}
