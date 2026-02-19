using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}