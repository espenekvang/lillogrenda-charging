using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;

namespace Lillogrenda.Charging.Infrastructure.Zaptec;

internal class ZaptecChargingSystem : IChargingSystem
{
    private readonly ZaptecClient _zaptecClient;

    public ZaptecChargingSystem(ZaptecClient zaptecClient)
    {
        _zaptecClient = zaptecClient;
    }
    
    public async Task<IEnumerable<ChargingSession>> GetChargingHistoryAsync(CancellationToken cancellationToken)
    {
        var chargeHistory = await _zaptecClient.GetChargeHistoryAsync(cancellationToken);
        return new List<ChargingSession>();
    }
}