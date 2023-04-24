namespace Lillogrenda.Charging.Domain.Entities;

public class ChargingHour
{
    public DateOnly Date { get; set; }
    public int Hour { get; set; }
    public double ChargeFactor { get; set; }
}