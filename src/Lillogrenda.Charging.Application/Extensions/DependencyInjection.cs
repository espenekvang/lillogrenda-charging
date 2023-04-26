using System.Reflection;
using Lillogrenda.Charging.Application.Invoices;
using Microsoft.Extensions.DependencyInjection;

namespace Lillogrenda.Charging.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient<ChargingHourExtractor>();
        services.AddTransient<InvoiceCalculator>();
        return services;
    }
}