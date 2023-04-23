namespace Lillogrenda.Charging.Infrastructure.Extensions;

public static class DateOnlyExtension
{
    public static string GetCacheKey(this DateOnly date)
    {
        return $"price-{date.ToString("d")}";
    }
}