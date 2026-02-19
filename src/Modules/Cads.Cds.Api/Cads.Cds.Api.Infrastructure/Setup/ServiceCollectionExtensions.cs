using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}