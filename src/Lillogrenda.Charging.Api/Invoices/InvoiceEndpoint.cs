using Lillogrenda.Charging.Application.Invoices.Queries;
using MediatR;

namespace Lillogrenda.Charging.Api.Invoices;

public class InvoiceEndpoint : EndpointWithoutRequest<string>
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
        await SendStringAsync("Invoices", cancellation: ct);
    }
}