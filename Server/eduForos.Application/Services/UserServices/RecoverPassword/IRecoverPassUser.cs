using eduForos.Domain.Entities;

namespace eduforos.Application.Services.UserServices.RecoverPassword;

public interface IRecoverPassUser
{
    bool RecoverPassword(User user);
    bool NewPassword(User user, string password);
}