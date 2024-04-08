namespace eduForos.Contracts.Course;

public record CourseAndUserCreateResult
{
    public string NameCourse { get; init; } = null!;
    public string UserName { get; init; } = null!;

}