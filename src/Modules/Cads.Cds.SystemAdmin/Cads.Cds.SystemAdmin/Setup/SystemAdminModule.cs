using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Cads.Cds.SystemAdmin.Application.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Setup;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Setup;

public sealed class SystemAdminModule : IModule
{
    public void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.AddSystemAdminInfrastructureLayer(config);

        services.AddSystemAdminApplicationLayer(config);
    }

    /// <summary>
    /// Add minimal API endpoints here. This excludes standard controllers.
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
    }
}