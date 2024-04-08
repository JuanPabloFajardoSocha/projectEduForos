using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services.Cloudinary;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace eduforos.Application.Services.UserServices.Edit;


public class EditUserService : IEditUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryService _cloudinaryServices;

    public EditUserService(IUserRepository userRepository, ICloudinaryService cloudinaryServices)
    {
        _userRepository = userRepository;
        _cloudinaryServices = cloudinaryServices;
    }

    public async Task EditUser(User user, IFormFile? file)
    {
        if (user.UserDocumentType != "CC" && user.UserDocumentType != "TI")
        {
            throw new ArgumentException("El tipo de documento es erróneo");
        }

        if (user.UserType != "Estudiante" && user.UserType != "Profesor" && user.UserType != "Administrador")
        {
            throw new ArgumentException("El rol de usuario no es válido");
        }
        if (user.PersonalEmail != null)
        {
            if (!user.PersonalEmail.Contains("@") || !user.PersonalEmail.EndsWith(".com"))
            {
                throw new ArgumentException("El correo electrónico no es válido");
            }
        }

        if (user.InstitutionalEmail == null || !user.InstitutionalEmail.Contains("@") || !user.InstitutionalEmail.EndsWith(".edu.co"))
        {
            throw new Exception("El correo electrónico institucional no es válido");
        }

        var existingUser = _userRepository.GetUserByInstitutionalEmail(user.InstitutionalEmail);
        Console.WriteLine("id usuario enviado" + user.IdUser);
        Console.WriteLine("id usuario buscado" + existingUser.IdUser);

        if (existingUser != null && existingUser.InstitutionalEmail == user.InstitutionalEmail
            && existingUser.IdUser != user.IdUser)
        {
            throw new Exception("El correo " + user.InstitutionalEmail + " ya está registrado en el sistema");
        }
        else
        {
            if (existingUser != null)
            {
                user.AssetId = existingUser.AssetId;
            }
        }

        if (file != null)
        {
            Console.WriteLine("la imagen no es null");
            if (file.Length > 1048576)
            {
                throw new Exception("El tamaño de la imagen es muy grande");
            }
            if (file.ContentType != "image/jpeg" && file.ContentType != "image/png" && file.ContentType != "image/jpg")
            {
                throw new Exception("El formato de la imagen no es válido");
            }

            if (user.AssetId != null)
            {
                var checkedFile = await _cloudinaryServices.CheckedFileAsync(user.AssetId);
                if (checkedFile)
                {
                    bool DeleteImage = await _cloudinaryServices.DeleteImageAsync(user.AssetId);
                    if (!DeleteImage)
                    {
                        throw new Exception("Error al eliminar la imagen");
                    }
                }

            }

            if (file != null)
            {
                var result = await _cloudinaryServices.UploadImageAsync(file, "eduForos/Users");
                if (result != null)
                {
                    user.AssetId = result.AssetId;
                    user.UrlPhoto = result.Url;
                }
                else
                {
                    throw new Exception("Error al guardar la imagen");
                }
            }

        }
        _userRepository.Edit(user);

    }





}