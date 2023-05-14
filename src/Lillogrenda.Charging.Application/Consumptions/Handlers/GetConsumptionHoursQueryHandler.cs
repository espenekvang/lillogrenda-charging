using Lillogrenda.Charging.Application.Consumptions.Queries;
using Lillogrenda.Charging.Domain.Entities;
using MediatR;

namespace Lillogrenda.Charging.Application.Consumptions.Handlers;

public class GetConsumptionHoursQueryHandler :IRequestHandler<GetConsumptionHoursQuery, IEnumerable<ConsumptionHour>>
{
    public Task<IEnumerable<ConsumptionHour>> Handle(GetConsumptionHoursQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enumerable.Empty<ConsumptionHour>());
    }
}