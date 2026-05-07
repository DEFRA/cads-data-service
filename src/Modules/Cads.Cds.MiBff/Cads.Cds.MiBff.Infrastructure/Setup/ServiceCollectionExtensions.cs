using Cads.Cds.MiBff.Infrastructure.Authorisation.Setup;
using Cads.Cds.MiBff.Infrastructure.Persistence.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffInfrastructureLayer(this IServiceCollection services)
    {
        services.ConfigureMiBffPersistence();
        services.ConfigureMiAuthorisation();

        return services;
    }
}