using Lillogrenda.Charging.Application.Invoices.Queries;
using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;
using MediatR;

namespace Lillogrenda.Charging.Application.Invoices.Handlers;

internal class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IEnumerable<Invoice>>
{
    private readonly IChargingSystem _chargingSystem;

    public GetInvoicesQueryHandler(IChargingSystem chargingSystem)
    {
        _chargingSystem = chargingSystem;
    }
    
    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        var history = await _chargingSystem.GetChargingHistoryAsync(cancellationToken);
        return Enumerable.Empty<Invoice>();
    }
}