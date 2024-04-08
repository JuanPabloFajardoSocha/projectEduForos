using eduForos.Domain.Entities;

namespace eduForos.Application.Common.Interfaces.Persistence;

public interface IMeetingRepository
{
    void CreateMeeting(VideoConference conference);
}