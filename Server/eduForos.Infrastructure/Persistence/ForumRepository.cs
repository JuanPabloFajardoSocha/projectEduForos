using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eduForos.Infrastructure.Persistence;

public class ForumRepository : IForumRepository
{
    private readonly EduForosDbContext _context;

    public ForumRepository(EduForosDbContext context)
    {
        _context = context;
    }

    public async Task<List<DataForumService>> GetForumByIdAsync(int id)
    {
        try
        {
            return await _context.Messages.Select(m => new DataForumService
            {
                Message = m,
                Forum = _context.Forums.FirstOrDefault(f => f.IdForum == m.IdForum),
                User = _context.Users.FirstOrDefault(u => u.IdUser == m.IdUser)
            }).Where(f => f.Forum.IdForum == id).ToListAsync();

        }
        catch (Exception e)
        {
            throw new Exception("Error al obtener el foro por id------->" + e.Message);
        }
    }
    
    public List<Forum> GetForumsByUser( Guid idUser)
    {
        try
        {
            
            return _context.Forums.Where(f=>f.IdUser==idUser).ToList();

        }
        catch (Exception e)
        {
            throw new Exception("Error al listar los foros------->" + e.Message);
        }
    }
    
    public async Task<Forum?> CreateForumAsync(Forum forum)
    {
        try
        {
            _context.Forums.Add(forum);
            await _context.SaveChangesAsync();
            return GetForumById(forum.IdForum);
        }
        catch (Exception e)
        {
            throw new Exception("Error al crear el foro ------->" + e);
        }
    }

    public Forum? GetForumById(int id)
    {
        try
        {
            return _context.Forums.SingleOrDefault(f => f.IdForum == id);

        }
        catch (Exception e)
        {
            throw new Exception("Error al obtener el foro por id------->" + e.Message);
        }
    }

    public async Task<bool> DeleteForum(int idForum)
    {
        try
        {
            var forum = _context.Forums.FirstOrDefault(f => f.IdForum == idForum);
            if (forum == null)
            {
                throw new Exception("No existe el foro");
            }
            _context.Forums.Remove(forum);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("Error al eliminar el foro------->" + e.Message);
        }
    }

    public List<Forum> GetForumsByCourse(Guid idUser)
    {
        var course = _context.UserCourses
           .Where(uc => uc.IdUser == idUser)
           .Join(_context.Courses,
                 uc => uc.IdCourse,
                 c => c.IdCourse,
                 (uc, c) => c)
                 .Distinct()
                 .FirstOrDefault();
        if (course == null)
        {
            throw new Exception("El usuario no esta registrado en un curso");
        }
        else
        {
           return _context.Forums.Where(f => f.IdCourse == course.IdCourse).ToList();
        }
        
    }
}