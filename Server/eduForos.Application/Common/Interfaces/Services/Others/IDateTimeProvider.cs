namespace eduForos.Application.Common.Interfaces.Services.Others;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}