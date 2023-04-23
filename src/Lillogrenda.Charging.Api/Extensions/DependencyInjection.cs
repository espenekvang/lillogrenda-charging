using FastEndpoints.Swagger;

namespace Lillogrenda.Charging.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddSwaggerDoc(settings =>
        {
            settings.Title = "Lillogrenda.Charging.Api";
            settings.Version = "v1";
        });
        return services;
    }
}