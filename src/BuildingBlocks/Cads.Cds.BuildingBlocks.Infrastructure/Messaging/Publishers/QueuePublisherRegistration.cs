using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Publishers;

public static class QueuePublisherRegistration
{
    public static void AddQueuePublishers(this IServiceCollection services, IConfigurationSection queueSection)
    {
        var queueConfigs = queueSection.Get<Dictionary<string, QueuePublisherOptions>>();
        if (queueConfigs == null) return;

        foreach (var (_, queueOptions) in queueConfigs)
        {
            services.AddQueuePublisherOptions(queueOptions);
        }
    }

    private static void AddQueuePublisherOptions(this IServiceCollection services, QueuePublisherOptions queueOptions)
    {
        services.Configure(queueOptions.Name, (QueuePublisherOptions opts) =>
        {
            opts.Name = queueOptions.Name;
            opts.QueueUrl = queueOptions.QueueUrl;
        });
    }
}