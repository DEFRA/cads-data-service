using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Setup;

/// <summary>
/// Module used to register global dependencies such as AWS clients.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddBuildBlocksModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddAmazonSQSDependencies(config);
    }
}