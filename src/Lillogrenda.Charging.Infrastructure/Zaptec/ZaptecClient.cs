using System.Text.Json;
using Lillogrenda.Charging.Infrastructure.Zaptec.Models;
using Microsoft.Extensions.Configuration;

namespace Lillogrenda.Charging.Infrastructure.Zaptec;

public class ZaptecClient
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public ZaptecClient(HttpClient client, IConfiguration configuration)
    {
        _client = client;
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

        var response = await _client.PostAsync("oauth/token", new FormUrlEncodedContent(values), cancellationToken);
        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseJson);

        return tokenResponse?.AccessToken ?? string.Empty;
    }

    public async Task<ChargeHistory> GetChargeHistoryAsync(CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync(cancellationToken);
        const string chargerId = "e4041e79-fd28-4110-bbe6-a7a4eac157f4";
        var from = new DateTime(2023, 01, 01);
        var to = DateTimeOffset.Now;
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = await _client.GetAsync($"api/chargehistory?ChargerId={chargerId}&From={from:s}&To={to:s}", cancellationToken);

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ChargeHistory>(responseJson) ?? new ChargeHistory();
        
    }
}