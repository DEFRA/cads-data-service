using Cads.Cds.BuildingBlocks.Application.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Files.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Setup;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Setup;

public sealed class BuildingBlocksModule : IModule
{
    public void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.AddAmazonS3Core(config);
        services.AddAmazonSQSCore(config);
        services.ConfigureDatabase(config);
        services.AddFileInfrastructure();
        services.AddBuildBlocksApplicationLayer();
    }

    /// <summary>
    /// No endpoints; this module provides shared infrastructure to the other modules.
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
    }
}