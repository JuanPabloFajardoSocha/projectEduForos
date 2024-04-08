using Microsoft.AspNetCore.Http;

namespace eduForos.Contracts.Forum.request;

public record CreateForumRequest
{
    public string NameForum { get; init; } = string.Empty;
    public string? Description { get; init; }
    public IFormFile? Photo { get; init; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }   
    public int IdSubject { get; init; }
    public int IdCourse { get; init; }
    public Guid IdUser { get; init; }


}