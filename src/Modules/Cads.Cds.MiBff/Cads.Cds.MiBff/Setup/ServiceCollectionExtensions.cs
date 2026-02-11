using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffModule(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}