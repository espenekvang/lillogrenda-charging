namespace Lillogrenda.Charging.Domain.Entities;

public class ConsumptionHour
{
    public DateOnly Date { get; set; }
    public int Hour { get; set; }
    public double KwhUsed { get; set; }
}