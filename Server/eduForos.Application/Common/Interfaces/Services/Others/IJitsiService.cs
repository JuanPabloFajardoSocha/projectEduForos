namespace eduForos.Application.Common.Interfaces.Services.Others;

public interface IJitsiService
{
    string GenerateJitsiToken(string roomName, string userName, string institutionalEmail, string urlPhoto);
}
