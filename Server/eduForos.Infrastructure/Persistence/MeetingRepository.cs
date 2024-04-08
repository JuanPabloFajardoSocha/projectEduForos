using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;

namespace eduForos.Infrastructure.Persistence;

public class MeetingRepository : IMeetingRepository
{
    private readonly EduForosDbContext _context;

    public MeetingRepository(EduForosDbContext context)
    {
        _context = context;
    }

    public void CreateMeeting(VideoConference meeting)
    {
        try
        {
            _context.VideoConferences.Add(meeting);

            _context.SaveChanges();
        }
        catch (Exception ex)
        {

            throw new Exception("Error en infraestructura----------------> " + ex);
        }
    }
}
