using eduForos.Domain.Entities;
using System.Security.Principal;

namespace eduForos.Application.Services.Authentication;

public interface IAuthenticationService
{
        AuthenticationResult Login(string institutionalEmail, string password);
}

