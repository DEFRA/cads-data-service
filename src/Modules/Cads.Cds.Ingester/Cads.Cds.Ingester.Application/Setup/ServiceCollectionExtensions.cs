using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngesterApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}