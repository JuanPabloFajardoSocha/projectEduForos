using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;

namespace eduForos.Application.Services.MeetingServices;

public class MeetingService : IMeetingService
{
    private readonly IMeetingRepository _meetingRepository;

    public MeetingService(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public void CreateMeeting(VideoConference meeting)
    {
        //Por ahora no se agregan validaciones

        _meetingRepository.CreateMeeting(meeting);
    }

}