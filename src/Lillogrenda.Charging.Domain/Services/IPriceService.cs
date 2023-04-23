using Lillogrenda.Charging.Domain.Entities;

namespace Lillogrenda.Charging.Domain.Services;

public interface IPriceService
{
    Task<IEnumerable<EnergyPrice>> GetPricesAsync(DateOnly date, CancellationToken cancellationToken);
}