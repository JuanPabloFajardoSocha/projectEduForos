using eduforos.Contracts.Authentication;
using eduForos.Application.Services.Authentication;
using eduForos.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace eduForos.Api.Controllers;

[Route("api")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        
        var authResult = _authenticationService.Login(
            request.InstitutionalEmail,
            request.Password
            );

        return Ok( new LoginResponse(
            authResult.UserResult.IdUser,
            authResult.UserResult.UserType,
            authResult.Token
            ) );    



    }
}
