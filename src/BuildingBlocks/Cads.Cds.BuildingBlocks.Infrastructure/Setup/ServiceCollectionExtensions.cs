using Cads.Cds.BuildingBlocks.Application.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Files.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBuildBlocksModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddAmazonS3Core(config);
        services.AddAmazonSQSCore(config);
        services.ConfigureDatabase(config);
        services.AddFileInfrastructure();
        services.AddBuildBlocksApplicationLayer();
        services.AddRepositories(config);

        return services;
    }
}