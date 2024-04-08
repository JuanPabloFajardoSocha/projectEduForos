namespace eduForos.Contracts.Meeting;

public record MeetingRequest
(
    string Name,
    string? Description,
    int IdCourse,
    int IdSubject,
    string Link,
    DateTime StartDate,
    DateTime EndDate

);

