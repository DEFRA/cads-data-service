using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Publishers;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.MiBff.Infrastructure.Messaging.Publishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddMiBffMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueuePublishers(configuration.GetSection(ModuleConfigurationSection.QueuesSectionName));

        services.Configure<OutboxDispatcherOptions>(
            configuration.GetSection(ModuleConfigurationSection.ReportJobOutboxDispatcherSectionName));

        services.AddQueueClients();

        services.AddMessagePublishers();

        services.AddOutboxDispatcher();
    }

    private static void AddQueueClients(this IServiceCollection services)
    {
        services.AddSingleton<MiBffFifoQueueClient>();
    }

    private static void AddMessagePublishers(this IServiceCollection services)
    {
        var retryPipeline = PublisherResiliencePipelines.CreateDefaultQueueRetryPipeline();

        services.AddSingleton<MiBffFifoQueuePublisher>();

        services.AddSingleton<IMessagePublisher<MiBffFifoQueueClient>>(sp =>
            new RetryingMessagePublisher<MiBffFifoQueueClient>(
                sp.GetRequiredService<MiBffFifoQueuePublisher>(),
                retryPipeline));
    }

    private static void AddOutboxDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IOutboxDispatcher<MiBffFifoQueueClient>, MiReportJobOutboxDispatcher>();
        services.AddHostedService<ReportJobOutboxDispatcherListener>();
    }
}


