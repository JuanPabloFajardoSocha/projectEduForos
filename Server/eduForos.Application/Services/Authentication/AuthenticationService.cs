using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services.Authentication;
using eduForos.Domain.Entities;

namespace eduForos.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenService _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenService jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string institutionalEmail, string password)
    {
        var userResponse = _userRepository.GetUserByInstitutionalEmail(institutionalEmail);

        if (userResponse != null && userResponse.Password == password)
        {
            var token = _jwtTokenGenerator.GenerateToken(userResponse.IdUser, userResponse.InstitutionalEmail, userResponse.UserType);

            return new AuthenticationResult(
                userResponse,
                token
                );
        }
        else
        {
            throw new Exception("Credenciales de acceso incorrectas");
        }
    }


}


/* public AuthenticationResult Login(string password, string institutionalEmail)
 {
     //1. Validate the user exists
     if (_userRepository.GetUserByInstitutionalEmail(institutionalEmail) is not User user)
     {
         throw new Exception("El usuario con el email proporcionado, no existe");
     }

     //2. Validate the password is correct

     if (user.Password != password)
     {
         throw new Exception("La contraseña es incorrecta");
     }

     //3. Create token
     var token = _jwtTokenGenerator.GenerateToken(user.IdUser, user.InstitutionalEmail);



     return new AuthenticationResult(
         user.IdUser,
         institutionalEmail,
         password,
         token);
 }*/

/*public AuthenticationResult Register(

    User user,
    token);


        //1. Validate the user doesn't exist
        if (_userRepository.GetUserByInstitutionalEmail(institutionalEmail) is not null)
        {
            throw new Exception("El usuario con el email proporcionado, ya existe");
        }

        //2. Create user (generate unique ID) & persist the user to DB

        var user = new User
        {
            UserDocumentType = userDocumentType,
            UserDocument = userDocument,
            Photo = photo,
            FirtsName = firstName,
            Surname = surname,
            Age = age,
            Address = address,
            Telephone = telephone,
            InstitutionalEmail = institutionalEmail,
            PersonalEmail = personalEmail,
            Password = password,
            UserType = userType,
            Profession = profession
        };

        _userRepository.Add(user);

        //3. Create token 

        var token = _jwtTokenGenerator.GenerateToken(user.IdUser, institutionalEmail);

        return new AuthenticationResult
        (
            user.IdUser,
            institutionalEmail,
            password,
            token
        );

    }
*/
