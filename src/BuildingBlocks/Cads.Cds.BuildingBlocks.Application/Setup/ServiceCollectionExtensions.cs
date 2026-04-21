using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBuildBlocksApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IRequestExecutor, RequestExecutor>();

        return services;
    }
}