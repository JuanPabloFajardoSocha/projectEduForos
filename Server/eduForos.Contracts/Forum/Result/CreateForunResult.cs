namespace eduForos.Contracts.Forum.request;

public record CreateForumResult
{
    public string? NameForum { get; init; }
    public string? Description { get; init; }
    public string? UrlPhoto { get; init; }
    public string? AssetId { get; init; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int IsGradable { get; set; }
    public int IdCourse { get; set; }
    public string? NameCourse { get; init; }
    public int IdSubject { get; set; }
    public string? NameSubject { get; init; }

    
}