using System.Text;
using System.Text.Json;
using Lillogrenda.Charging.Domain.Entities;
using Lillogrenda.Charging.Domain.Services;
using Lillogrenda.Charging.Infrastructure.Extensions;
using Lillogrenda.Charging.Infrastructure.HvaKosterStrommen.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Lillogrenda.Charging.Infrastructure.HvaKosterStrommen;

internal class HvaKosterStrommenPriceService : IPriceService
{
    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _cache;

    public HvaKosterStrommenPriceService(HttpClient httpClient, IDistributedCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<IEnumerable<EnergyPrice>> GetPricesAsync(DateOnly date, CancellationToken cancellationToken)
    {
        var responseJson = await GetPriceJsonAsync(date, cancellationToken);
        var prices = JsonSerializer.Deserialize<IEnumerable<Price>>(responseJson);

        return prices?
            .Select(price => new EnergyPrice(price.TimeStart, price.TimeEnd, price.NokPerKwh, Currency.Nok))
            .ToList() ?? new List<EnergyPrice>();
    }

    private async Task<string> GetPriceJsonAsync(DateOnly date, CancellationToken cancellationToken)
    {
        var cacheHit = await _cache.GetAsync(date.GetCacheKey(), cancellationToken);

        if (cacheHit != null)
        {
            return Encoding.UTF8.GetString(cacheHit);
        }
        
        var response = await _httpClient.GetAsync($"api/v1/prices/{date.Year}/{date.Month.ToString().PadLeft(2,'0')}-{date.Day.ToString().PadLeft(2, '0')}_NO1.json",
            cancellationToken);
        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        await _cache.SetAsync(date.GetCacheKey(), Encoding.UTF8.GetBytes(responseJson), token: cancellationToken);

        return responseJson;
    }
}