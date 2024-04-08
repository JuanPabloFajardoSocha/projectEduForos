using eduForos.Domain.Entities;

namespace eduForos.Application.Common.Interfaces.Services;

public record DataForumService{
    public Forum? Forum { get; set; }
    public Message? Message { get; set; }
    public User? User { get; set; }

} 