using eduForos.Application.Common.Interfaces.Services.Others;


namespace eduforos.Infrastructure.Settings.Others;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
