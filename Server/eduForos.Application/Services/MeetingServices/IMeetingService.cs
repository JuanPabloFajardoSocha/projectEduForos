using eduForos.Domain.Entities;

namespace eduForos.Application.Services.MeetingServices;

public interface IMeetingService
{
    void CreateMeeting(VideoConference meeting);
}