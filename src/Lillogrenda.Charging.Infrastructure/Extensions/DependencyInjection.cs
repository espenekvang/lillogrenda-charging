using Lillogrenda.Charging.Domain.Services;
using Lillogrenda.Charging.Infrastructure.HvaKosterStrommen;
using Lillogrenda.Charging.Infrastructure.Zaptec;
using Microsoft.Extensions.DependencyInjection;

namespace Lillogrenda.Charging.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddTransient<IChargingSystem, ZaptecChargingSystem>();
        services.AddTransient<IPriceService, HvaKosterStrommenPriceService>(provider => provider.GetRequiredService<HvaKosterStrommenPriceService>());
        
        services.AddHttpClient<ZaptecClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.zaptec.com/");
        });
        services.AddHttpClient<HvaKosterStrommenPriceService>(client =>
        {
            client.BaseAddress = new Uri("https://www.hvakosterstrommen.no/");
        });
        
        return services;
    }
}