using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Health;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;

public static class QueueConsumerRegistration
{
    public static void AddQueueConsumers(this IServiceCollection services, IConfigurationSection queueSection)
    {
        var queueConfigs = queueSection.Get<Dictionary<string, QueueConsumerOptions>>();
        if (queueConfigs == null) return;

        foreach (var (_, queueOptions) in queueConfigs)
        {
            services.AddQueueConsumer(queueOptions);
        }
    }

    private static void AddQueueConsumer(this IServiceCollection services, QueueConsumerOptions queueOptions)
    {
        services.Configure(queueOptions.Name, (QueueConsumerOptions opts) =>
        {
            opts.Name = queueOptions.Name;
            opts.QueueUrl = queueOptions.QueueUrl;
            opts.DlqQueueUrl = queueOptions.DlqQueueUrl;
            opts.MaxNumberOfMessages = queueOptions.MaxNumberOfMessages;
            opts.WaitTimeSeconds = queueOptions.WaitTimeSeconds;
            opts.HealthcheckEnabled = queueOptions.HealthcheckEnabled;
        });

        if (queueOptions.HealthcheckEnabled)
        {
            services.AddTransient<AwsSqsHealthCheck<QueueConsumerOptions>>();

            services.AddHealthChecks()
                .AddCheck<AwsSqsHealthCheck<QueueConsumerOptions>>(
                    name: queueOptions.Name,
                    tags: ["aws", "sqs"]
                );
        }

        // Register poller
        // Register DLQ service
        // Register observers
        // Register hosted service (runs all pollers)
    }
}