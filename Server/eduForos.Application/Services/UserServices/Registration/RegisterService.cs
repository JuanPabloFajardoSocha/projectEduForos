using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services.Cloudinary;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace eduforos.Application.Services.UserServices.Registration;


public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryService _cloudinaryServices;

    public RegisterService(IUserRepository userRepository, ICloudinaryService cloudinaryServices)
    {
        _userRepository = userRepository;
        _cloudinaryServices = cloudinaryServices;
    }

    public async Task<RegisterResult> Register(
        User registerResult,
        IFormFile? photo
    )
    {
        if (registerResult.UserDocument != null)
        {
            if (registerResult.UserDocument.Length < 10 || registerResult.UserDocument.Length > 11)
            {
                throw new Exception("El documento no debe sobrepasar los 10 caracteres ni ser menor a 10 caracteres");
            }
        }
        else
        {
            throw new Exception("El documento no puede ser nulo");
        }

        if (registerResult.UserDocumentType != "CC" && registerResult.UserDocumentType != "TI")
        {
            throw new Exception("El tipo de documento es erróneo");
        }

        if (registerResult.UserType != null)
        {
            if (registerResult.UserType == "Estudiante" || registerResult.UserType == "Administrador")
            {
                if (registerResult.Profession != "N/A")
                {
                    throw new Exception("La profesión no es válida para el tipo de usuario " + registerResult.UserType);
                }
            }
            else if (registerResult.UserType == "Profesor")
            {
                if (registerResult.Profession == "N/A")
                {
                    throw new Exception("La profesión no es válida para el tipo de usuario " + registerResult.UserType);
                }
            }
            else
            {
                throw new Exception("El rol de usuario no es válido");
            }
        }

        var existingUser = _userRepository.GetUserByInstitutionalEmail(registerResult.InstitutionalEmail);
        if (existingUser != null)
        {
            throw new Exception("El correo " + registerResult.InstitutionalEmail + " ya está registrado en el sistema");
        }

        var existingDocument = _userRepository.GetUserByDocument(registerResult.UserDocument);
        if (existingDocument != null)
        {
            throw new Exception("El usuario con documento " + registerResult.UserDocument + " ya está registrado en el sistema");
        }

        if (registerResult.PersonalEmail != null)
        {
            if (!registerResult.PersonalEmail.Contains('@') || !registerResult.PersonalEmail.EndsWith(".com"))
            {
                throw new ArgumentException("El correo electrónico no es válido");
            }
        }

        if (registerResult.InstitutionalEmail == null || !registerResult.InstitutionalEmail.Contains('@') || !registerResult.InstitutionalEmail.EndsWith(".edu.co"))
        {
            throw new Exception("El correo electrónico institucional no es válido");
        }

        if (photo != null)
        {
            if (photo.Length > 1048576)
            {
                throw new Exception("El tamaño de la imagen es muy grande");
            }

            if (photo.ContentType != "image/png" && photo.ContentType != "image/jpg" && photo.ContentType != "image/jpeg")
            {
                throw new Exception("El formato de la imagen no es válido. Solo se permiten formatos .png, .jpg y .jpeg");
            }
            var imageUrl = await _cloudinaryServices.UploadImageAsync(photo, "eduForos/Users");

            registerResult.AssetId = imageUrl.AssetId;
            registerResult.UrlPhoto = imageUrl.Url;
        }

        registerResult.IdUser = Guid.NewGuid();
        _userRepository.Add(registerResult);

        return new RegisterResult
        (
            registerResult.IdUser,
            registerResult.UserType
        );
    }

}