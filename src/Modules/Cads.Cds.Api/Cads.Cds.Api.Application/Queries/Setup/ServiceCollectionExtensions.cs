using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;
using Cads.Cds.Api.Application.Queries.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Application.Queries.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddQueryAdapters(this IServiceCollection services)
    {
        services.AddScoped<AnimalDetailsQueryAdapter>();
        services.AddScoped<AnimalsOnCphQueryAdapter>();
        services.AddScoped<LocationsQueryAdapter>();
    }
}