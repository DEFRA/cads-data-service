using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Cads.Cds.Ingester.Application.Setup;
using Cads.Cds.Ingester.Infrastructure.Setup;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Setup;

public sealed class IngesterModule : IModule
{
    public void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.AddIngesterInfrastructureLayer(config);

        services.AddIngesterApplicationLayer();
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        // No minimal API endpoints; this module runs as background services.
    }
}