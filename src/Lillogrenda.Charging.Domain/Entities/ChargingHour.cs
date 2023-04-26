namespace Lillogrenda.Charging.Domain.Entities;

public class ChargingHour
{
    public DateOnly Date { get; set; }
    public int Hour { get; set; }
    public double MinutesCharged { get; set; }
    public double PricePerkWh { get; set; }
    public double KwPerMinute { get; set; }
    public double KwCharged => KwPerMinute * MinutesCharged;
    public double Amount => KwCharged * PricePerkWh;
}