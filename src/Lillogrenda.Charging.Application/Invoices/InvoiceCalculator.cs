using Lillogrenda.Charging.Domain.Entities;

namespace Lillogrenda.Charging.Application.Invoices;

internal class InvoiceCalculator
{
    private readonly ChargingHourExtractor _chargingHourExtractor;

    public InvoiceCalculator(ChargingHourExtractor chargingHourExtractor)
    {
        _chargingHourExtractor = chargingHourExtractor;
    }

    public async Task<IEnumerable<InvoiceLine>> CalculateForAsync(IEnumerable<ChargingSession> chargingSessions,
        CancellationToken cancellationToken)
    {
        var invoiceLines = new List<InvoiceLine>();

        foreach (var chargingSession in chargingSessions)
        {
            var chargingHours = await _chargingHourExtractor.ExtractFromAsync(chargingSession, cancellationToken);
            invoiceLines.Add(new InvoiceLine
            {
                Start = chargingSession.Start,
                End = chargingSession.End,
                EnergyInkWh = chargingSession.EnergyInkWh,
                Amount = chargingHours.Sum(hour => hour.Amount)
            });
        }

        return invoiceLines;
    }
}