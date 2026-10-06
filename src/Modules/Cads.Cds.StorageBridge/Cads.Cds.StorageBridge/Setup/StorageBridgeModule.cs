using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Cads.Cds.StorageBridge.Application.Setup;
using Cads.Cds.StorageBridge.Endpoints;
using Cads.Cds.StorageBridge.Infrastructure.Setup;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Setup;

public sealed class StorageBridgeModule : IModule
{
    public void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.AddStorageBridgeInfrastructureLayer(config);

        services.AddStorageBridgeApplicationLayer();
    }

    /// <summary>
    /// Add minimal API endpoints here. This excludes standard controllers.
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapStorageBridgeStorageManagementEndpoints();
    }
}