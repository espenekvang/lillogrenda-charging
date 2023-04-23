using Lillogrenda.Charging.Domain.Services;
using Lillogrenda.Charging.Infrastructure.Zaptec;
using Microsoft.Extensions.DependencyInjection;

namespace Lillogrenda.Charging.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IChargingSystem, ZaptecChargingSystem>();
        services.AddHttpClient<ZaptecClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.zaptec.com/");
        });
        return services;
    }
}