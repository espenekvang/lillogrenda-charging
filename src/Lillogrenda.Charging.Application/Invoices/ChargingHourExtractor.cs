using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;

namespace Lillogrenda.Charging.Application.Invoices;

internal class ChargingHourExtractor
{
    private readonly IPriceService _priceService;

    public ChargingHourExtractor(IPriceService priceService)
    {
        _priceService = priceService;
    }
    
    public IEnumerable<ChargingHour> ExtractFrom(IEnumerable<ChargingSession> chargingSessions)
    {
        return chargingSessions.SelectMany(GetChargingHours);
    }

    private static IEnumerable<ChargingHour> GetChargingHours(ChargingSession chargingSession)
    {
        var nextHour = chargingSession.Start
            .AddHours(1)
            .AddMicroseconds(-chargingSession.Start.Microsecond)
            .AddMilliseconds(-chargingSession.Start.Millisecond)
            .AddSeconds(-chargingSession.Start.Second)
            .AddMinutes(-chargingSession.Start.Minute);
        
        var chargingHours = new List<ChargingHour>();
        var chargingHour = new ChargingHour
        {
            Date = new DateOnly(chargingSession.Start.Year, chargingSession.Start.Month, chargingSession.Start.Day),
            Hour = chargingSession.Start.Hour,
            ChargeFactor = GetChargeFactor(chargingSession.Start, nextHour)
        };
        
        chargingHours.Add(chargingHour);

        while (nextHour <= chargingSession.End)
        {
            chargingHours.Add(new ChargingHour
            {
                Date = new DateOnly(nextHour.Year, nextHour.Month, nextHour.Day),
                Hour = nextHour.Hour,
                ChargeFactor = GetChargeFactor(nextHour, chargingSession.End)
            });
            nextHour = nextHour.AddHours(1);
        }

        return chargingHours;
    }

    private static double GetChargeFactor(DateTimeOffset from, DateTimeOffset to)
    {
        var duration = to - from;
        var oneHour = new TimeSpan(0, 1, 0, 0);
        if (duration >= oneHour)
        {
            return 1;
        }

        return duration.TotalSeconds / oneHour.TotalSeconds;
    }
}