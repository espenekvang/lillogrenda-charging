using Lillogrenda.Charging.Domain.Entities;

namespace Lillogrenda.Charging.Domain.Services;

public interface IChargingSystem
{
    Task<IEnumerable<ChargingSession>> GetChargingHistoryAsync(string chargerId, DateOnly from, DateOnly to,
        CancellationToken cancellationToken);
}