using System.Runtime.InteropServices.JavaScript;

namespace Lillogrenda.Charging.Domain.Entities;

public class ChargingSession
{
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public double EnergyInkWh { get; set; }

    public double GetEnergyPerHour()
    {
        var duration = End - Start;
        return EnergyInkWh/duration.TotalHours;
    }

    public IEnumerable<ChargingHour> GetChargingHours()
    {
        var nextHour = Start
            .AddHours(1)
            .AddMicroseconds(-Start.Microsecond)
            .AddMilliseconds(-Start.Millisecond)
            .AddSeconds(-Start.Second)
            .AddMinutes(-Start.Minute);
        
        var chargingHours = new List<ChargingHour>();
        var chargingHour = new ChargingHour
        {
            Date = new DateOnly(Start.Year, Start.Month, Start.Day),
            Hour = Start.Hour,
            ChargeFactor = GetChargeFactor(Start, nextHour)
        };
        
        chargingHours.Add(chargingHour);

        while (nextHour <= End)
        {
            chargingHours.Add(new ChargingHour
            {
                Date = new DateOnly(nextHour.Year, nextHour.Month, nextHour.Day),
                Hour = nextHour.Hour,
                ChargeFactor = GetChargeFactor(nextHour, End)
            });
            nextHour = nextHour.AddHours(1);
        }

        return chargingHours;
    }

    private double GetChargeFactor(DateTimeOffset from, DateTimeOffset to)
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