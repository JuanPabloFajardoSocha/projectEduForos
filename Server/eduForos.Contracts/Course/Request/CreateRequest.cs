namespace eduForos.Contracts.Course;

public record CreateRequest
(
    string Name,
    string? Description
);