using eduForos.Domain.Entities;

namespace eduForos.Application.Services.Authentication;


public record AuthenticationResult
(
    User UserResult,
    string Token
);



