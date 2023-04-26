using Lillogrenda.Charging.Application.Invoices.Queries;
using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;
using MediatR;

namespace Lillogrenda.Charging.Application.Invoices.Handlers;

internal class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IEnumerable<InvoiceLine>>
{
    private readonly IChargingSystem _chargingSystem;
    private readonly IPriceService _priceService;
    private readonly InvoiceCalculator _invoiceCalculator;

    public GetInvoicesQueryHandler(
        IChargingSystem chargingSystem, 
        IPriceService priceService, 
        InvoiceCalculator invoiceCalculator)
    {
        _chargingSystem = chargingSystem;
        _priceService = priceService;
        _invoiceCalculator = invoiceCalculator;
    }
    
    public async Task<IEnumerable<InvoiceLine>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        var chargingSessions = await _chargingSystem.GetChargingHistoryAsync(
            "e4041e79-fd28-4110-bbe6-a7a4eac157f4",
            new DateOnly(2023, 01, 01), 
            new DateOnly(2023, 04, 26), 
            cancellationToken);
        return await _invoiceCalculator.CalculateForAsync(chargingSessions, cancellationToken);
    }
}

