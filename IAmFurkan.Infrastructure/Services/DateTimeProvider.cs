using IAmFurkan.Application.Common.Interfaces.Services;

namespace IAmFurkan.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
