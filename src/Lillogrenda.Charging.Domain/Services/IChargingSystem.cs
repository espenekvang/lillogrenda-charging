using Lillogrenda.Charging.Domain.Entities;

namespace Lillogrenda.Charging.Domain.Services;

public interface IChargingSystem
{
    Task<IEnumerable<ChargingSession>> GetChargingHistoryAsync(CancellationToken cancellationToken);
}