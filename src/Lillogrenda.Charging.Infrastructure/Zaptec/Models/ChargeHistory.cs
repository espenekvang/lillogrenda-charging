namespace Lillogrenda.Charging.Infrastructure.Zaptec.Models;

public class ChargeHistory
{
    public int Pages { get; init; }
    public List<ChargingSession> Data { get; init; } = default!;
}

public class ChargingSession
{
    public DateTimeOffset StartDateTime { get; init; } = default!;
    public DateTimeOffset EndDateTime { get; init; } = default!;
    public double Energy { get; init; }
}
