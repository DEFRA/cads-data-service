using Cads.Cds.Api.Application.Queries.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Application.Queries.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddQueryAdapters(this IServiceCollection services)
    {
        services.AddScoped<LocationsQueryAdapter>();
    }
}