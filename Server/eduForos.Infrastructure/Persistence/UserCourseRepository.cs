using System.Data.Common;
using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Services.CourseService;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eduForos.Infrastructure.Persistence;

public class UserCourseRepository : IUserCourseRepository
{
    private readonly EduForosDbContext _context;
    public UserCourseRepository(EduForosDbContext context)
    {
        _context = context;
    }

    public bool AddUserToCourse(User user, Course course)
    {
        Boolean status = false;
        UserCourse userCourse = new UserCourse();
        userCourse.IdCourse = course.IdCourse;
        userCourse.IdUser = user.IdUser;

        if (user.UserType == "Profesor")
        {
            var exi = _context.UserCourses.FirstOrDefault(uc => uc.IdUser == user.IdUser && uc.IdCourse==course.IdCourse);
            if (exi != null)
            {
                throw new Exception("El usuario ya esta registrado en el curso");
            }
            else
            {
                try
                {
                    _context.UserCourses.Add(userCourse);
                    status = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                finally
                {
                    _context.SaveChanges();
                }
            }
        }
        else
        {
            var exist = _context.UserCourses.FirstOrDefault(uc => uc.IdUser == user.IdUser);

            if (exist != null)
            {
                throw new Exception("El usuario ya esta registrado en un curso");
            }
            else
            {
                try
                {
                    _context.UserCourses.Add(userCourse);
                    status = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                finally
                {
                    _context.SaveChanges();
                }
            }
        }

        return status;
    }

    public List<User> GetUsersByCourse(int idCourse)
    {

        var users = _context.UserCourses
            .Where(uc => uc.IdCourse == idCourse)
            .Join(_context.Users,
                  uc => uc.IdUser,
                  u => u.IdUser,
                  (uc, u) => u)
                  .Distinct()
                  .ToList();

        if (users.Any())
        {
            return users;
        }
        else
        {
            return null;
        }

    }


    public List<Course> GetCoursesByUser(Guid idUser)
    {

        var courses = _context.UserCourses
            .Where(uc => uc.IdUser == idUser)
            .Join(_context.Courses,
                  uc => uc.IdCourse,
                  c => c.IdCourse,
                  (uc, c) => c)
                  .Distinct()
                  .ToList();

        if (courses.Any())
        {
            return courses;
        }
        else
        {
            return null;
        }

    }


    public bool DeleteUserToCourse(int idCourse, Guid idUser)
    {
        bool status = true;

        UserCourse userCourse = _context.UserCourses.FirstOrDefault(uc => uc.IdCourse == idCourse && uc.IdUser == idUser);
        
        if (userCourse != null)
        {
            try
            {
                _context.UserCourses.Remove(userCourse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                status = false;
            }
            finally
            {
                _context.SaveChanges();
            }
            
        }

        return status;
    }
}
