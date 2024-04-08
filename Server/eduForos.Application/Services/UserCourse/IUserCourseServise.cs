
using eduForos.Domain.Entities;

namespace eduForos.Application.Services.UserCourseServise;

public interface IUserCourseService{
    Boolean AddParticipant(int idCourse, Guid idUser);
    List<User> GetUsersByCourse(int idCourse);
    Boolean DeleteUserToCourse(int idCourse, Guid idUser);
    List<Course> GetCoursesByUser(Guid idUser);

}