using Cads.Cds.ApiSurface.Messages.Imports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.StorageBridge.Application.Messaging.Clients;
using Cads.Cds.StorageBridge.Core.Configuration;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Consumers;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Factories;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Serializers;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.Messaging.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddStorageBridgeMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueueConsumers(configuration.GetSection(ModuleConfigurationSection.QueuesSectionName));

        services.AddMessageHandlers();

        services.AddMessageSerializers();

        services.AddMessageConsumers();
    }

    private static void AddMessageHandlers(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var registry = new StorageBridgeMessageCommandRegistry();

            registry.Register<S3ToPostgresCopyMessageCommandFactory>(nameof(S3ToPostgresCopyMessage).ReplaceSuffix());

            return registry;
        });
    }

    private static void AddMessageSerializers(this IServiceCollection services)
    {
        var messageIdentifierTypes = new[]
        {
            typeof(S3ToPostgresCopyMessage)
        };

        foreach (var messageType in messageIdentifierTypes)
        {
            var typeInfo = StorageBridgeSerializerContext.Default.GetType().GetProperty(messageType.Name)?.GetValue(StorageBridgeSerializerContext.Default);

            var serializerType = typeof(MessageIdentifierSerializer<>).MakeGenericType(messageType);
            var interfaceType = typeof(IUnwrappedMessageSerializer<>).MakeGenericType(messageType);

            services.AddSingleton(interfaceType, Activator.CreateInstance(serializerType, typeInfo)!);
        }
    }

    private static void AddMessageConsumers(this IServiceCollection services)
    {
        services.AddHostedService<StorageBridgeFifoQueueListener>()
            .AddSingleton<IQueuePoller<StorageBridgeFifoQueueClient>, StorageBridgeFifoQueuePoller>();

        services.AddSingleton<IQueueAdminService<StorageBridgeFifoQueueClient>, StorageBridgeFifoQueueAdminService>();
    }
}