namespace Lillogrenda.Charging.Domain.Entities;

public record ChargingSession(DateTimeOffset Start, DateTimeOffset End, float Energy);