using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Setup;

// A vertical module owns both halves of its own composition: the services it registers before the
// container is built, and the endpoints it maps once the application is built.
public interface IModule
{
    void AddServices(IServiceCollection services, IConfiguration config);

    void MapEndpoints(IEndpointRouteBuilder app);
}