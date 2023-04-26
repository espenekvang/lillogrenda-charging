namespace Lillogrenda.Charging.Domain.Entities;

public class InvoiceLine
{
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public double EnergyInkWh { get; set; }
    public double Amount { get; set; }
}