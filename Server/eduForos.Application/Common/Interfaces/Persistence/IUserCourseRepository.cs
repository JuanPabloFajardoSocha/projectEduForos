using eduForos.Application.Services.CourseService;
using eduForos.Domain.Entities;

namespace eduForos.Application.Common.Interfaces.Persistence;

public interface IUserCourseRepository{
    Boolean AddUserToCourse(User user, Course course);
    List<User> GetUsersByCourse(int id);

    Boolean DeleteUserToCourse(int idCourse, Guid idUser);

    List<Course> GetCoursesByUser(Guid idUser);



}