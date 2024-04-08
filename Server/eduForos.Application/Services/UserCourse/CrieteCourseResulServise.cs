using eduForos.Domain.Entities;

namespace eduForos.Application.Services.CourseService;

public class UserData
{
    public User? User { get; set; }
    public UserCourse? UserCourse { get; set; }
    public Course? Course { get; set; }
}