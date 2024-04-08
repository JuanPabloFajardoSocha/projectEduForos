using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eduForos.Infrastructure.Persistence;

public class CourseRepository : ICourseRepository
{
    private readonly EduForosDbContext _context;
    public CourseRepository(EduForosDbContext context)
    {
        _context = context;
    }

    public bool Add(Course course)
    {
        try
        {
            var exist = _context.Courses.Any(c => c.Name == course.Name);
            if (exist)
            {
                throw new Exception("El curso ya existe");
            }
            else
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return true;
            }
        }
        catch (Exception e)
        {
            throw new Exception("Error al Añadir curso ------->" + e.Message);
        }
    }

    public List<Course> List()
    {
        try
        {
            return _context.Courses.ToList();
        }
        catch (Exception e)
        {
            throw new Exception("Error al listar curso ------------>" + e.Message);
        }
    }

    public Course? GetCourseById(int id)
    {
        Console.WriteLine("id del curso ----------------------------> "+id);
        try
        {
            var course = _context.Courses.SingleOrDefault(c => c.IdCourse == id);
            if (course == null)
            {
                throw new Exception("El curso no existe");
            }
            return course;
        }
        catch (Exception e)
        {
            throw new Exception("Error al obtener el curso por id------->" + e.Message);
        }
    }

    public string Update(Course course)
    {
        try
        {
            var result = _context.Courses
            .AsNoTracking()
            .SingleOrDefault(c => c.IdCourse == course.IdCourse)?.Name;

            if (result == null)
            {
                throw new Exception("El curso no existe");
            }
            else
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                return result;
            }
        }
        catch (Exception e)
        {
            throw new Exception("Error al actualizar el curso------>" + e.Message);
        }
    }

    public Course Delete(int id)
    {
        try
        {
            var course = _context.Courses.SingleOrDefault(c => c.IdCourse == id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
                return course;
            }
            else
            {
                throw new Exception("El curso no existe");
            }
        }
        catch
        {
            throw new Exception("Error al eliminar el curso -------------------------->");
        }

    }

}