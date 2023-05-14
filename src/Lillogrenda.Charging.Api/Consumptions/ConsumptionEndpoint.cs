using Lillogrenda.Charging.Application.Consumptions.Queries;
using Lillogrenda.Charging.Domain.Entities;
using MediatR;

namespace Lillogrenda.Charging.Api.Consumptions;

public class ConsumptionEndpoint : EndpointWithoutRequest<IEnumerable<ConsumptionHour>>
{
    private readonly IMediator _mediator;

    public ConsumptionEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get("/consumptions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var consumptionHours = await _mediator.Send(new GetConsumptionHoursQuery(), ct);
        await SendAsync(consumptionHours, cancellation: ct);
    }
}