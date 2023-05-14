using Lillogrenda.Charging.Domain.Entities;
using MediatR;

namespace Lillogrenda.Charging.Application.Consumptions.Queries;

public class GetConsumptionHoursQuery : IRequest<IEnumerable<ConsumptionHour>>
{
    
}