using System.Text.Json;
using Lillogrenda.Charging.Infrastructure.Zaptec.Models;
using Microsoft.Extensions.Configuration;

namespace Lillogrenda.Charging.Infrastructure.Zaptec;

public class ZaptecClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ZaptecClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    private async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        var values = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", _configuration["zaptec-username"]! },
            { "password", _configuration["zaptec-password"]! }
        };

        var response = await _httpClient.PostAsync("oauth/token", new FormUrlEncodedContent(values), cancellationToken);
        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseJson);

        return tokenResponse?.AccessToken ?? string.Empty;
    }

    public async Task<ChargeHistory> GetChargeHistoryAsync(string chargerId, DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync(cancellationToken);
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.GetAsync($"api/chargehistory?ChargerId={chargerId}&From={from:s}&To={to:s}", cancellationToken);

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ChargeHistory>(responseJson) ?? new ChargeHistory();
        
    }
}