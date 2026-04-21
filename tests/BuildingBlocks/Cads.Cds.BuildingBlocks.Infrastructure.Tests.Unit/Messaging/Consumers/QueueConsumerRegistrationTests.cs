using Amazon.SQS;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Health;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Messaging.Consumers;

public class QueueConsumerRegistrationTests
{
    private static ServiceProvider BuildProvider(IDictionary<string, string> configValues)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                configValues.ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value)
            )
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();

        var sqsMock = new Mock<IAmazonSQS>();
        services.AddSingleton(sqsMock.Object);

        var section = configuration.GetSection("Queues");
        services.AddQueueConsumers(section);

        return services.BuildServiceProvider();
    }

    [Fact]
    public void DoesNothing_When_Section_Is_Missing()
    {
        var provider = BuildProvider(new Dictionary<string, string>());

        var options = provider.GetRequiredService<
            Microsoft.Extensions.Options.IOptions<HealthCheckServiceOptions>>().Value;

        options.Registrations.Should().BeEmpty();
    }

    [Fact]
    public void Registers_Queue_Options_For_Each_Queue()
    {
        var provider = BuildProvider(new Dictionary<string, string>
        {
            ["Queues:QueueA:Name"] = "QueueA",
            ["Queues:QueueA:QueueUrl"] = "url-a",
            ["Queues:QueueA:DlqQueueUrl"] = "dlq-a",
            ["Queues:QueueA:MaxNumberOfMessages"] = "5",
            ["Queues:QueueA:WaitTimeSeconds"] = "2",
            ["Queues:QueueA:HealthcheckEnabled"] = "false"
        });

        var opts = provider.GetRequiredService<
            Microsoft.Extensions.Options.IOptionsMonitor<QueueConsumerOptions>>();

        var queueA = opts.Get("QueueA");

        queueA.Name.Should().Be("QueueA");
        queueA.QueueUrl.Should().Be("url-a");
        queueA.DlqQueueUrl.Should().Be("dlq-a");
        queueA.MaxNumberOfMessages.Should().Be(5);
        queueA.WaitTimeSeconds.Should().Be(2);
        queueA.HealthcheckEnabled.Should().BeFalse();
    }

    [Fact]
    public void Registers_HealthCheck_When_HealthcheckEnabled_Is_True()
    {
        var provider = BuildProvider(new Dictionary<string, string>
        {
            ["Queues:QueueA:Name"] = "QueueA",
            ["Queues:QueueA:QueueUrl"] = "url-a",
            ["Queues:QueueA:DlqQueueUrl"] = "dlq-a",
            ["Queues:QueueA:MaxNumberOfMessages"] = "5",
            ["Queues:QueueA:WaitTimeSeconds"] = "2",
            ["Queues:QueueA:HealthcheckEnabled"] = "true"
        });

        var options = provider.GetRequiredService<
            Microsoft.Extensions.Options.IOptions<HealthCheckServiceOptions>>().Value;

        options.Registrations.Should().ContainSingle(r => r.Name == "QueueA");

        var registration = options.Registrations.Single(r => r.Name == "QueueA");

        registration.Tags.Should().Contain(["aws", "sqs"]);
        registration.Factory(provider).Should().BeOfType<AwsSqsHealthCheck<QueueConsumerOptions>>();
    }

    [Fact]
    public void DoesNot_Register_HealthCheck_When_HealthcheckEnabled_Is_False()
    {
        var provider = BuildProvider(new Dictionary<string, string>
        {
            ["Queues:QueueA:Name"] = "QueueA",
            ["Queues:QueueA:QueueUrl"] = "url-a",
            ["Queues:QueueA:DlqQueueUrl"] = "dlq-a",
            ["Queues:QueueA:MaxNumberOfMessages"] = "5",
            ["Queues:QueueA:WaitTimeSeconds"] = "2",
            ["Queues:QueueA:HealthcheckEnabled"] = "false"
        });

        var options = provider.GetRequiredService<
            Microsoft.Extensions.Options.IOptions<HealthCheckServiceOptions>>().Value;

        options.Registrations.Should().BeEmpty();
    }
}