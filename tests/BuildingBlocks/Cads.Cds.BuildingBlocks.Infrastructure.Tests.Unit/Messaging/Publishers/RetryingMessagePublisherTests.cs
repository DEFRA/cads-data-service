using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Publishers;
using FluentAssertions;
using Moq;
using Polly;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Messaging.Publishers;

public class RetryingMessagePublisherTests
{
    private readonly Mock<IMessagePublisher<TestFifoQueueClient>> _innerMock = new();
    private static readonly FifoMessageMetadata s_metadata = new("Group", "Dedup", "CorrelationId");

    private RetryingMessagePublisher<TestFifoQueueClient> CreateSut(ResiliencePipeline? pipeline = null) =>
        new(_innerMock.Object, pipeline ?? PublisherResiliencePipelines.CreateDefaultQueueRetryPipeline());

    [Fact]
    public void QueueUrl_ShouldReturnInnerPublisherQueueUrl()
    {
        _innerMock.Setup(x => x.QueueUrl).Returns("https://example.com/queue");
        var sut = CreateSut();

        sut.QueueUrl.Should().Be("https://example.com/queue");
    }

    [Fact]
    public async Task PublishAsync_ShouldCallInnerPublisher_Once_WhenSuccessful()
    {
        var message = new { Content = "hello" };
        _innerMock
            .Setup(x => x.PublishAsync(message, s_metadata, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var sut = CreateSut();
        await sut.PublishAsync(message, s_metadata, TestContext.Current.CancellationToken);

        _innerMock.Verify(x => x.PublishAsync(message, s_metadata, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task PublishAsync_ShouldRetry_WhenInnerPublisherThrowsTransientPublishFailedException()
    {
        var message = new { Content = "hello" };
        var attempts = 0;

        _innerMock
            .Setup(x => x.PublishAsync(message, s_metadata, It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                attempts++;
                if (attempts < 3)
                {
                    throw new PublishFailedException("transient failure", isTransient: true);
                }
                return Task.CompletedTask;
            });

        var sut = CreateSut(PublisherResiliencePipelines.CreateDefaultQueueRetryPipeline(maxRetryAttempts: 3));
        await sut.PublishAsync(message, s_metadata, TestContext.Current.CancellationToken);

        attempts.Should().Be(3); // 1 initial attempt + 2 retries before succeeding
    }

    [Fact]
    public async Task PublishAsync_ShouldNotRetry_WhenInnerPublisherThrowsNonTransientPublishFailedException()
    {
        var message = new { Content = "hello" };
        var attempts = 0;

        _innerMock
            .Setup(x => x.PublishAsync(message, s_metadata, It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                attempts++;
                throw new PublishFailedException("permanent failure", isTransient: false);
            });

        var sut = CreateSut();
        var act = async () => await sut.PublishAsync(message, s_metadata, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<PublishFailedException>();
        attempts.Should().Be(1);
    }

    [Fact]
    public async Task PublishAsync_ShouldThrowAfterExhaustingRetries_WhenAlwaysTransient()
    {
        var message = new { Content = "hello" };
        var attempts = 0;

        _innerMock
            .Setup(x => x.PublishAsync(message, s_metadata, It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                attempts++;
                throw new PublishFailedException("always transient", isTransient: true);
            });

        var sut = CreateSut(PublisherResiliencePipelines.CreateDefaultQueueRetryPipeline(maxRetryAttempts: 2));
        var act = async () => await sut.PublishAsync(message, s_metadata, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<PublishFailedException>();
        attempts.Should().Be(3); // 1 initial attempt + 2 retries, all failing
    }

    public class TestFifoQueueClient : IQueueClient
    {
        public string ClientName => GetType().Name;
    }
}