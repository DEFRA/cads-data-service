using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiModule(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}
