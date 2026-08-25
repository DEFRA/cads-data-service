using Cads.Cds.Api.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Cads.Cds.Ingester.Setup;
using Cads.Cds.MiBff.Setup;
using Cads.Cds.StorageBridge.Setup;
using Cads.Cds.SystemAdmin.Setup;

namespace Cads.Cds.Setup;

public static class ModuleRegistration
{
    // BuildingBlocks registers the shared infrastructure the other modules build on, so it stays first.
    // Assembly scanning explicitly ruled out so manual control over the order of middleware execution can live here.
    private static readonly IModule[] s_modules =
    [
        new BuildingBlocksModule(),
        new ApiModule(),
        new IngesterModule(),
        new MiBffModule(),
        new StorageBridgeModule(),
        new SystemAdminModule()
    ];

    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration config)
    {
        foreach (var module in s_modules)
        {
            module.AddServices(services, config);
        }

        return services;
    }

    public static IEndpointRouteBuilder MapModules(this IEndpointRouteBuilder app)
    {
        foreach (var module in s_modules)
        {
            module.MapEndpoints(app);
        }

        return app;
    }
}