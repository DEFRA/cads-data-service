using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.Ingester.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Infrastructure.Messaging.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddMessagingDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueueConsumers(configuration.GetSection(ModuleConfigurationSection.QueuesSectionName));
    }
}