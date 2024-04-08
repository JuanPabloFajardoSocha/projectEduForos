namespace eduForos.Contracts.Course;

public record EditRequest
(
    string Name,
    string? Description
);