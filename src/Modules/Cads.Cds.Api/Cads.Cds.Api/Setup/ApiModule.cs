using Cads.Cds.Api.Application.Setup;
using Cads.Cds.Api.Infrastructure.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Setup;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Setup;

public sealed class ApiModule : IModule
{
    public void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.AddApiInfrastructureLayer();

        services.AddApiApplicationLayer();
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        // Controllers and SOAP endpoints are wired by the host pipeline.
    }
}