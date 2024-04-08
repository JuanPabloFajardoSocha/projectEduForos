

using eduForos.Domain.Entities;
namespace eduForos.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(User user);

    User? GetUserByInstitutionalEmail(string institutionalEmail);

    User? GetUserByDocument(string userDocument);

    List<User> GetRegisteredUsersByType(string userType);

    void Edit(User user);

    Boolean DeleteUser(Guid IdUser);

    User? GetUserById(Guid IdUser);

    User? SearchRecoverPassword(User user);
}