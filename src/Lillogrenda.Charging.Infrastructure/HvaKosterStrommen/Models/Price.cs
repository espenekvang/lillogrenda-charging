using System.Text.Json.Serialization;

namespace Lillogrenda.Charging.Infrastructure.HvaKosterStrommen.Models;

internal class Price
{
    [JsonPropertyName("NOK_per_kWh")] public double NokPerKwh { get; init; }
    [JsonPropertyName("EUR_per_kWh")] public double EurPerKwh { get; init; }
    [JsonPropertyName("EXR")] public double Exr { get; init; }
    [JsonPropertyName("time_start")] public DateTimeOffset TimeStart { get; init; }
    [JsonPropertyName("time_end")] public DateTimeOffset TimeEnd { get; init; }
}