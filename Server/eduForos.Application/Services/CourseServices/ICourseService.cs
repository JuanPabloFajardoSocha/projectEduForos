using eduForos.Domain.Entities;

namespace eduForos.Application.Services.CourseService;

public interface ICourseService
{
    List<Course> List();
    void AddCourse(Course course);
    Course? GetCourseById(int id);
    string? UpdateCourse(Course course);
    Course? DeleteCourse(int id);
}