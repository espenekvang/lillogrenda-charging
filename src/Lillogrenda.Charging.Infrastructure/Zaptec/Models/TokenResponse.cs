using System.Text.Json.Serialization;

namespace Lillogrenda.Charging.Infrastructure.Zaptec.Models;

internal class TokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; init; } = default!;
    [JsonPropertyName("token_type")] public string TokenType { get; init; } = default!;
    [JsonPropertyName("expires_in")] public int ExpiresIn { get; init; }
}