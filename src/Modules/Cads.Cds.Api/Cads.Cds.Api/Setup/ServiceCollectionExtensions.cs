using Cads.Cds.Api.Application.Setup;
using Cads.Cds.Api.Infrastructure.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddApiInfrastructureLayer(config);

        services.AddApiApplicationLayer();
        return services;
    }
}