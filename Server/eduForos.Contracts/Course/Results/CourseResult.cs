namespace eduForos.Contracts.Course;

public record CourseResult
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; } = null!;
    
} 