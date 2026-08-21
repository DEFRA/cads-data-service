using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Cads.Cds.MiBff.Application.Setup;
using Cads.Cds.MiBff.Controllers.Authorisation.Setup;
using Cads.Cds.MiBff.Infrastructure.Setup;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Setup;

public sealed class MiBffModule : IModule
{
    public void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.AddReportAuthorizationProviders();

        services.AddMiBffInfrastructureLayer();

        services.AddMiBffApplicationLayer(config);
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        // No minimal API endpoints; this module exposes controllers.
    }
}