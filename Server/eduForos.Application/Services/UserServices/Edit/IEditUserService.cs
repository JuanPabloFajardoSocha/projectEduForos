using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace eduforos.Application.Services.UserServices.Edit;

public interface IEditUserService
{
    public Task EditUser(
        User editResult, IFormFile? file
    );
}