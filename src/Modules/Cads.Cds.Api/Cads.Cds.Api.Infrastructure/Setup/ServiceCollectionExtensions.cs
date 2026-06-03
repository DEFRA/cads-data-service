using Cads.Cds.Api.Infrastructure.Persistence.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiInfrastructureLayer(this IServiceCollection services)
    {
        services.ConfigureApiPersistence();

        return services;
    }
}