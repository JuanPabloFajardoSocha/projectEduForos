using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services.Authentication;
using eduForos.Application.Common.Interfaces.Services.Others;
using eduForos.Domain.Entities;

namespace eduforos.Application.Services.UserServices.RecoverPassword;

public class RecoverPassUser : IRecoverPassUser
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _email;

    public RecoverPassUser(IUserRepository userRepository, IJwtTokenService jwtTokenService, IEmailService email)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _email = email;
    }

    public bool RecoverPassword(User user)
    {

        if (user == null)
        {
            throw new Exception("Debe ingresar un usuario");
        }
        else
        {
            if (user.InstitutionalEmail == null && user.PersonalEmail == null)
            {
                throw new Exception("Debe ingresar un correo institucional o personal");
            }
            else
            {
                if (user.InstitutionalEmail == null && user.PersonalEmail != null)
                {
                    user.InstitutionalEmail = user.PersonalEmail;
                }
                else if (user.PersonalEmail == null && user.InstitutionalEmail != null)
                {
                    user.PersonalEmail = user.InstitutionalEmail;
                }
                else
                {
                    throw new Exception("Debe ingresar un correo institucional o personal");
                }

                if (!user.InstitutionalEmail.Contains("@") || !user.PersonalEmail.Contains("@") &&
                    !user.InstitutionalEmail.Contains(".com") || !user.PersonalEmail.Contains(".com"))
                {
                    throw new Exception("Debe ingresar un correo valido");
                }

                var result = _userRepository.SearchRecoverPassword(user);

                if (result != null)
                {
                    var data = _jwtTokenService.GenerateToken(result.IdUser, user.InstitutionalEmail, result.UserType);
                    if (data != null)
                    {
                        string ruta = "https://localhost:5001/api/user/newPassword/" + data;

                        var mensaje = "Hola, " + result.FirtsName + " " + result.SurName + " para recuperar tu contraseña ingresa al siguiente link: " + ruta;
                        var asunto = "Recuperar contraseña";
                        var email = result.InstitutionalEmail;

                        return _email.SentEmail(email, asunto, mensaje);

                    }
                    else
                    {
                        throw new Exception("Error al generar el token");
                    }
                }
                else
                {
                    throw new Exception("El usuario no existe");
                }
            }
        }
    }

    public bool NewPassword(User user, string token)
    {

        var data = _jwtTokenService.DecodeToken(token);
        if (data != null)
        {
            var idUserClaim = data.Claims.FirstOrDefault(claim => claim.Type == "sub") ?? throw new Exception("Token JWT incompleto: reclamos faltantes.");
            var idUser = idUserClaim.Value;
            // var institutionalEmail = emailClaim.Value;
            // var userType = userTypeClaim.Value;

            var search = _userRepository.GetUserById(Guid.Parse(idUser));

            if (search != null)
            {
                search.Password = user.Password;
                _userRepository.Edit(search);

                return true;
            }
            else
            {
                throw new Exception("El usuario no existe!!!");
            }
        }
        else
        {
            throw new Exception("Error al decodificar el token");
        }


    }

}