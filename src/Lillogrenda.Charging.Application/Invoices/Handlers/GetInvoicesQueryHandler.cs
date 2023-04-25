using Lillogrenda.Charging.Application.Invoices.Queries;
using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;
using MediatR;

namespace Lillogrenda.Charging.Application.Invoices.Handlers;

internal class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IEnumerable<Invoice>>
{
    private readonly IChargingSystem _chargingSystem;
    private readonly IPriceService _priceService;
    private readonly ChargingHourExtractor _chargingHourExtractor;

    public GetInvoicesQueryHandler(IChargingSystem chargingSystem, IPriceService priceService, ChargingHourExtractor chargingHourExtractor)
    {
        _chargingSystem = chargingSystem;
        _priceService = priceService;
        _chargingHourExtractor = chargingHourExtractor;
    }
    
    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        var chargingSessions = await _chargingSystem.GetChargingHistoryAsync(
            "e4041e79-fd28-4110-bbe6-a7a4eac157f4",
            new DateOnly(2023, 01, 01), 
            new DateOnly(2023, 04, 23), 
            cancellationToken);
        var chargingHours = _chargingHourExtractor.ExtractFrom(chargingSessions);
        var prices = await _priceService.GetPricesAsync(new DateOnly(2023, 04, 23), cancellationToken);
        var prices2 = await _priceService.GetPricesAsync(new DateOnly(2023, 04, 23), cancellationToken);
        return Enumerable.Empty<Invoice>();
    }
}

