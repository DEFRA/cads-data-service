using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}