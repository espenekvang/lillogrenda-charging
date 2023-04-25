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
} 