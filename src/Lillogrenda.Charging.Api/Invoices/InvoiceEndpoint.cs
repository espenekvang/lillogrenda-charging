namespace Lillogrenda.Charging.Api.Invoices;

public class InvoiceEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("/invoices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendStringAsync("Invoices", cancellation: ct);
    }
}