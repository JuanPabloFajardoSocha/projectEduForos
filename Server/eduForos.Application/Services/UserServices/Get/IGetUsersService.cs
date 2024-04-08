using eduForos.Domain.Entities;

namespace eduforos.Application.Services.UserServices.Get;

public interface IGetUsersService
{
    public List<User> GetRegisteredUsers(string UserType);
    public User GetUserById(string IdUser);

}