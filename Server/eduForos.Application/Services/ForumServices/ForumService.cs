using System.IdentityModel.Tokens.Jwt;
using eduforos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services.Authentication;
using eduForos.Application.Common.Interfaces.Services.Cloudinary;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace eduForos.Application.Services.ForumServices;

public class ForumService : IForumService
{
    private readonly IForumRepository _forumRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ICourseRepository _courseRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public ForumService(IForumRepository forumRepository, IJwtTokenService jwtTokenService,
        ISubjectRepository subjectRepository, ICloudinaryService cloudinaryService,
        ICourseRepository courseRepository, IMessageRepository messageRepository)
    {
        _forumRepository = forumRepository;
        _jwtTokenService = jwtTokenService;
        _subjectRepository = subjectRepository;
        _cloudinaryService = cloudinaryService;
        _courseRepository = courseRepository;
        _messageRepository = messageRepository;
    }

    public async Task<bool> DeleteForum(int idForum)
    {
        var data = _forumRepository.GetForumById(idForum) ?? throw new Exception("No existe el foro");

        if (data == null)
        {
            throw new Exception("No hay foros para eliminar");
        }

        if (data.UrlPhoto != null)
        {
            await _cloudinaryService.DeleteImageAsync(data.AssetId);
        }

        return await _forumRepository.DeleteForum(idForum);
    }
    public List<Forum> GetForumsByUser(Guid idUser)
    {
        var forums = _forumRepository.GetForumsByUser(idUser);
        if (forums==null)
        {
            throw new Exception("No hay foros registrados para este usuario");
        }

        return forums;
    }
    public async Task<Forum?> CreateForumAsync(Forum foro, IFormFile? file)
    {

        if (foro.Name == null)
        {
            throw new Exception("El nombre del foro no puede ser vacio");
        }

        if (foro.IdCourse==null || foro.IdCourse == 0)
        {
            throw new Exception("Deve seleccionar un curso para el foro");
        }
        else
        {
            var exitsCourse = _courseRepository.GetCourseById(foro.IdCourse) ?? throw new Exception("El curso no existe");

            if (exitsCourse == null)
            {
                throw new Exception("El curso no existe");
            }
        }

        if (foro.IdSubject == null || foro.IdSubject==0)
        {
            throw new Exception("La tematica del foro no puede ser vacia");
        }


        if (file != null )
        {            
            if (file.ContentType != "image/jpeg" && file.ContentType != "image/png"
                && file.ContentType != "image/jpg" && file.ContentType != "image/gif")
            {
                throw new Exception("El formato de la imagen no es valido");
            }

            if (file.Length > 1048576)
            {
                throw new Exception("El tamaño de la imagen es muy grande");
            }

            var img = await _cloudinaryService.UploadImageAsync(file, "Forum");

            if (img != null)
            {
                foro.UrlPhoto = img.Url;
                foro.AssetId = img.AssetId;
            }

        }

        return  await _forumRepository.CreateForumAsync(foro) ?? throw new Exception("No se pudo crear el foro");
        

    }

    public List<Forum?> GetForumsByCourse(Guid idUser)
    {
        var response=_forumRepository.GetForumsByCourse(idUser);
        if (response == null)
        {
            throw new Exception("No hay foros registrados en el curso");
        }
        else
        {
            return response;
        }
    }
}