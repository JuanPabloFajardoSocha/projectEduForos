namespace eduForos.Application.Common.Interfaces.Services.Others;

public interface IEmailService
{
    bool SentEmail(string emailSent, string asunto, string mensaje);
}