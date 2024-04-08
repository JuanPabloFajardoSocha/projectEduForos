using eduForos.Domain.Entities;

namespace eduForos.Application.Common.Interfaces.Persistence;

public interface ICourseRepository
{
    List<Course> List();
    bool Add(Course course);
    Course? GetCourseById(int id);
    string? Update(Course course);
    Course? Delete(int id);
}