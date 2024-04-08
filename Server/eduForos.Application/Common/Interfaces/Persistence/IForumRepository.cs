using eduForos.Application.Common.Interfaces.Services;
using eduForos.Domain.Entities;

namespace eduForos.Application.Common.Interfaces.Persistence;

public interface IForumRepository
{
    Task<Forum?> CreateForumAsync(Forum forum);
    List<Forum> GetForumsByUser(Guid idUser);
    Task<List<DataForumService>> GetForumByIdAsync(int id);
    Forum? GetForumById(int id);
    Task<bool> DeleteForum(int idForum);

    List<Forum> GetForumsByCourse(Guid idUser);

}
