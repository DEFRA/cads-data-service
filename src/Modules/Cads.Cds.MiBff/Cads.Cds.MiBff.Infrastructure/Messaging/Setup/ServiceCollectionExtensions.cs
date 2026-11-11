using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Core.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Infrastructure.Messaging.Consumers;
using Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.MiBff.Infrastructure.Messaging.Factories;
using Cads.Cds.MiBff.Infrastructure.Messaging.Publishers;
using Cads.Cds.MiBff.Infrastructure.Messaging.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddMiBffMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueuePublishers(configuration.GetSection(ModuleConfigurationSection.QueuesSectionName));

        services.AddQueueConsumers(configuration.GetSection(ModuleConfigurationSection.QueuesSectionName));

        services.Configure<OutboxDispatcherOptions>(
            configuration.GetSection(ModuleConfigurationSection.ReportJobOutboxDispatcherSectionName));

        services.AddQueueClients();

        services.AddMessagePublishers();

        services.AddOutboxDispatcher();

        services.AddMessageHandlers();

        services.AddMessageSerializers();

        services.AddMessageConsumers();
    }

    private static void AddMessageHandlers(this IServiceCollection services)
    {
        services.AddSingleton(_ =>
            new MessageCommandRegistry<MiBffFifoQueueClient>()
                .Register<ReportJobRequestedMessageCommandFactory>(nameof(ReportJobRequestedMessage).ReplaceSuffix()));
    }

    private static void AddMessageSerializers(this IServiceCollection services)
    {
        services.AddSingleton<IUnwrappedMessageSerializer<ReportJobRequestedMessage>>(
            new MessageIdentifierSerializer<ReportJobRequestedMessage>(
                MiBffSerializerContext.Default.ReportJobRequestedMessage));
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
    
    private static void AddMessageConsumers(this IServiceCollection services)
    {
        services.AddHostedService<MiBffReportFifoQueueListener>()
            .AddSingleton<IQueuePoller<MiBffFifoQueueClient>, MiBffReportFifoQueuePoller>();

        services.AddSingleton<IQueueAdminService<MiBffFifoQueueClient>, DeadLetterQueueAdminService<MiBffFifoQueueClient>>();
    }
}