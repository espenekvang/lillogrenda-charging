using Lillogrenda.Charging.Application.Invoices.Queries;
using Lillogrenda.Charging.Domain.Entities;
using MediatR;

namespace Lillogrenda.Charging.Application.Invoices.Handlers;

internal class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IEnumerable<Invoice>>
{
    public Task<IEnumerable<Invoice>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enumerable.Empty<Invoice>());
    }
}