using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;

namespace eduforos.Application.Services.UserServices.Get;


public class GetUsersService : IGetUsersService
{
    private readonly IUserRepository _userRepository;

    public GetUsersService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<User> GetRegisteredUsers(string UserType)
    {
        List<User> Users;
        if (!UserType.Equals("Administrador") && !UserType.Equals("Profesor") && !UserType.Equals("Estudiante"))
        {
            throw new Exception("El tipo de usuario especificado no es valido");
        }
        else
        {
            Users = _userRepository.GetRegisteredUsersByType(UserType);
        }
        return Users;

    }

    public User GetUserById(string IdUser)
    {
        Guid idUser;
        try
        {
            idUser = Guid.Parse(IdUser);
        }
        catch (Exception ex)
        {
            throw new Exception("Id de usuario incorrecto");
        }
        User user = _userRepository.GetUserById(idUser);

        if (user == null)
        {
            throw new Exception("El usuario no se encuentra registrado");
        }
        else
        {
            return user;
        }
    }


}








