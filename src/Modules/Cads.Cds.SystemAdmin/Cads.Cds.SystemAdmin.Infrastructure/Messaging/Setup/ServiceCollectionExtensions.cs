using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Publishers;
using Cads.Cds.SystemAdmin.Application.Messaging.Clients;
using Cads.Cds.SystemAdmin.Core.Configuration;
using Cads.Cds.SystemAdmin.Infrastructure.Messaging.Publishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Messaging.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddSystemAdminMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueuePublishers(configuration.GetSection(ModuleConfigurationSection.QueuesSectionName));

        services.AddQueueClients();

        services.AddMessagePublishers();
    }

    private static void AddQueueClients(this IServiceCollection services)
    {
        services.AddSingleton<SystemAdminFifoQueueClient>();
    }

    private static void AddMessagePublishers(this IServiceCollection services)
    {
        var retryPipeline = PublisherResiliencePipelines.CreateDefaultQueueRetryPipeline();

        services.AddSingleton<SystemAdminFifoQueuePublisher>();

        services.AddSingleton<IMessagePublisher<SystemAdminFifoQueueClient>>(sp =>
            new RetryingMessagePublisher<SystemAdminFifoQueueClient>(
                sp.GetRequiredService<SystemAdminFifoQueuePublisher>(),
                retryPipeline));
    }
}