namespace Lillogrenda.Charging.Domain.Entities;

public record EnergyPrice(DateTimeOffset Start, DateTimeOffset End, double PricePerkWh, Currency Currency);