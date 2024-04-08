namespace eduForos.Contracts.Course;

public record CourseOnUserResult
{
    public int CourseId { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public List<ParticipantsOnCourse> Participants { get; init; } = new List<ParticipantsOnCourse>();

}

public record ParticipantsOnCourse
{
    public int UserId { get; init; }
    public string FirstName { get; init; } = null!;
    public string SurName { get; init; } = null!;
    public string Profession { get; init; } = null!;
    
} 