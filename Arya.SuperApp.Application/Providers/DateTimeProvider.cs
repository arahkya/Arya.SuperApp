using Arya.SuperApp.Application.Interfaces.Date;

namespace Arya.SuperApp.Application.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}