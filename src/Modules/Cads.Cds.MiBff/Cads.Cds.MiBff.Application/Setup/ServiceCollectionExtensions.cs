using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}