using eduForos.Domain.Entities;

namespace eduforos.Application.Services.UserServices.Get;

public record GetUsersResult
(
    List<User> Users
);