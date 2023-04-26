using Lillogrenda.Charging.Application.Invoices.Queries;
using Lillogrenda.Charging.Domain.Entities;
using MediatR;

namespace Lillogrenda.Charging.Api.Invoices;

public class InvoiceEndpoint : EndpointWithoutRequest<IEnumerable<InvoiceLine>>
{
    private readonly IMediator _mediator;

    public InvoiceEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get("/invoices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var invoices = await _mediator.Send(new GetInvoicesQuery(), ct);
        await SendAsync(invoices, cancellation:ct);
    }
}