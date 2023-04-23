using Lillogrenda.Charging.Domain.Entities;

namespace Lillogrenda.Charging.Domain.Services;

public interface IChargingSystem
{
    IEnumerable<ChargingSession> GetChargingHistory();
}