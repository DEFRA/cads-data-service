using Cads.Cds.MiBff.Application.Setup;
using Cads.Cds.MiBff.Infrastructure.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddMiBffInfrastructureLayer(config);

        services.AddMiBffApplicationLayer(config);

        return services;
    }
}