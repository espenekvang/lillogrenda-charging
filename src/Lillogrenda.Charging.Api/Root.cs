namespace Lillogrenda.Charging.Api;

public class Root : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendStringAsync("Lillogrenda.Charging v1.0", cancellation: ct);
    }
}