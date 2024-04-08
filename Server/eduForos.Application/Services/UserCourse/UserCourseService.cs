
using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Services.CourseService;
using eduForos.Application.Services.UserCourseServise;
using eduForos.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace eduforos.Application.Services.UserCourseServise;

public class UserCourseServise : IUserCourseService
{

    private readonly IUserCourseRepository _userCourseRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;

    public UserCourseServise(IUserCourseRepository userCourseRepository, IUserRepository userRepository, ICourseRepository courseRepository)
    {
        _userCourseRepository = userCourseRepository;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
    }

    public Boolean AddParticipant(int idCourse, Guid idUser)
    {      
        User user = _userRepository.GetUserById(idUser);
        Course course = _courseRepository.GetCourseById(idCourse);
        Boolean status = false;


        if (user == null)
        {
            throw new Exception("El usuario no esta registrado");
        }
        else if(course == null)
        {
            throw new Exception("El curso no esta registrado");
        }
        else
        {
            if (_userCourseRepository.AddUserToCourse(user, course))
            {
                status = true;
            }
        }
        
        return status;
    }

    public bool DeleteUserToCourse(int idCourse, Guid idUser)
    {
       bool status = true; 
       var response =_userCourseRepository.DeleteUserToCourse(idCourse, idUser);
        if (!response)
        {
            status = false;
        }
        
        return status;

    }

    public List<User> GetUsersByCourse(int idCourse)
    {
       var users= _userCourseRepository.GetUsersByCourse(idCourse);
        if(users != null)
        {
            return users;
        }
        else
        {
            throw new Exception("No hay usuarios registrados en este curso");
        }
    }

    public List<Course> GetCoursesByUser(Guid idUser)
    {
        var courses = _userCourseRepository.GetCoursesByUser(idUser);
        if (courses != null)
        {
            return courses;
        }
        else
        {
            throw new Exception("No hay cursos registrados para este usuario");
        }
    }
}