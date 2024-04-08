namespace eduForos.Application.Services.ForumServices;

public record ListForumServiceResult
{
    public int IdForum { get; init; }
    public string? NameForum { get; init; }
    public string? Description { get; init; }
    public string? UrlPhoto { get; init; }
    public string? AssetId { get; init; }
    public int IdCourse { get; set; }
    public string? NameCourse { get; init; }
    public Guid? IdUser { get; set; }
    public string? NameUser { get; init; }
    public bool Status { get; init; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int IsGradable { get; set; }

}