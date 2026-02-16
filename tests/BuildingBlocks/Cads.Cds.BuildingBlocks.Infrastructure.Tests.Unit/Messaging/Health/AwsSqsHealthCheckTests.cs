using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Health;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Messaging.Health;

public class AwsSqsHealthCheckTests
{
    private readonly Mock<IAmazonSQS> _amazonSQSMock = new();
    private readonly Mock<IOptionsMonitor<QueueConsumerOptions>> _optionsMonitorMock = new();
    private readonly HealthCheckContext _healthCheckContext = new();

    private readonly QueueConsumerOptions _queueConsumerOptions = new()
    {
        Name = "TestQueue",
        QueueUrl = "http://localhost:4566/000000000000/test-queue",
        WaitTimeSeconds = 5,
        MaxNumberOfMessages = 10
    };

    private readonly AwsSqsHealthCheck<QueueConsumerOptions> _sut;

    public AwsSqsHealthCheckTests()
    {
        _optionsMonitorMock
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(_queueConsumerOptions);

        _amazonSQSMock
            .Setup(x => x.GetQueueAttributesAsync(It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetQueueAttributesResponse { HttpStatusCode = HttpStatusCode.OK });

        _sut = new AwsSqsHealthCheck<QueueConsumerOptions>(_optionsMonitorMock.Object, _amazonSQSMock.Object);

        _healthCheckContext = new HealthCheckContext
        {
            Registration = new HealthCheckRegistration(
                name: "TestQueue",
                instance: _sut,
                failureStatus: null,
                tags: null)
        };
    }

    [Fact]
    public async Task GivenValidQueueName_WhenCallingCheckHealthAsync_ShouldSucceed()
    {
        var result = await _sut.CheckHealthAsync(_healthCheckContext, CancellationToken.None);

        result.Status.Should().Be(HealthStatus.Healthy);
    }

    [Fact]
    public async Task GivenQueueAttributesAreMissing_WhenCallingCheckHealthAsync_ShouldReturnDegraded()
    {
        _amazonSQSMock
            .Setup(x => x.GetQueueAttributesAsync(It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        var result = await _sut.CheckHealthAsync(_healthCheckContext, CancellationToken.None);

        result.Status.Should().Be(HealthStatus.Degraded);
    }

    [Fact]
    public async Task GivenGetQueueAttributesFails_WhenCallingCheckHealthAsync_ShouldReturnUnhealthy()
    {
        _amazonSQSMock
            .Setup(x => x.GetQueueAttributesAsync(It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Service call failed."));

        var result = await _sut.CheckHealthAsync(_healthCheckContext, CancellationToken.None);

        result.Status.Should().Be(HealthStatus.Unhealthy);
    }

    [Fact]
    public async Task GivenGetQueueAttributesTimesOut_WhenCallingCheckHealthAsync_ShouldReturnUnhealthy()
    {
        _amazonSQSMock
            .Setup(x => x.GetQueueAttributesAsync(It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new TaskCanceledException("Task has been cancelled"));

        var result = await _sut.CheckHealthAsync(_healthCheckContext, CancellationToken.None);

        result.Status.Should().Be(HealthStatus.Unhealthy);
        result.Exception.Should().NotBeNull().And.BeOfType<TimeoutException>();
        result.Exception.Message.Should().Be($"The queue check was cancelled, probably because it timed out after {_queueConsumerOptions.WaitTimeSeconds} seconds");
    }
}