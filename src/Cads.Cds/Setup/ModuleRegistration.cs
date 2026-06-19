using Cads.Cds.Api.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Cads.Cds.Ingester.Setup;
using Cads.Cds.MiBff.Setup;
using Cads.Cds.StorageBridge.Setup;
using Cads.Cds.SystemAdmin.Setup;

namespace Cads.Cds.Setup;

public static class ModuleRegistration
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration config)
    {
        services.AddBuildBlocksModule(config);

        services.AddApiModule(config);
        services.AddIngesterModule(config);
        services.AddMiBffModule(config);
        services.AddStorageBridgeModule(config);
        services.AddSystemAdminModule(config);

        return services;
    }
}