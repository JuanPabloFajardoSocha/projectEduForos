using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace eduForos.Application.Services.ForumServices;

public interface IForumService
{
    Task<Forum?> CreateForumAsync(Forum request, IFormFile? file);
    List<Forum?> GetForumsByUser(Guid idUser);
    Task<bool> DeleteForum(int idForum);
    List<Forum?> GetForumsByCourse(Guid idUser);

}