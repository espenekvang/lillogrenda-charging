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
    
    public async Task<IEnumerable<ChargingHour>> ExtractFromAsync(ChargingSession chargingSession, CancellationToken cancellationToken)
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
            MinutesCharged = GetMinutesCharged(chargingSession.Start, nextHour),
            PricePerkWh = await GetPricePerKwhAsync(chargingSession.Start, cancellationToken),
            KwPerMinute = chargingSession.GetKwPerMinute()
        };
        
        chargingHours.Add(chargingHour);

        while (nextHour <= chargingSession.End)
        {
            chargingHours.Add(new ChargingHour
            {
                Date = new DateOnly(nextHour.Year, nextHour.Month, nextHour.Day),
                Hour = nextHour.Hour,
                MinutesCharged = GetMinutesCharged(nextHour, chargingSession.End < nextHour.AddHours(1) ? chargingSession.End : nextHour.AddHours(1)),
                PricePerkWh = await GetPricePerKwhAsync(nextHour, cancellationToken),
                KwPerMinute = chargingSession.GetKwPerMinute()
            });
            nextHour = nextHour.AddHours(1);
        }

        return chargingHours;
    }

    private async Task<double> GetPricePerKwhAsync(DateTimeOffset start, CancellationToken cancellationToken)
    {
        var prices = await _priceService.GetPricesAsync(new DateOnly(start.Year, start.Month, start.Day),
            cancellationToken);
        var price = prices.First(p => p.Start.Hour == start.Hour);
        return price.PricePerkWh;
    }

    private static double GetMinutesCharged(DateTimeOffset from, DateTimeOffset to)
    {
        var duration = to - from;
        return duration.TotalMinutes;
    }
}