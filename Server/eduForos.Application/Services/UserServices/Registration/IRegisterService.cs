using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace eduforos.Application.Services.UserServices.Registration;

public interface IRegisterService
{
    public Task<RegisterResult> Register(
        User registerResult,
        IFormFile? photo
        );

}