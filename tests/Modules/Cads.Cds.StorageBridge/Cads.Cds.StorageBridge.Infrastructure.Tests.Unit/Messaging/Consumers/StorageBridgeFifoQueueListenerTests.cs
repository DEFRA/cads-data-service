using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.StorageBridge.Application.Messaging.Clients;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Consumers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Messaging.Consumers;

public class StorageBridgeFifoQueueListenerTests
{
    private readonly Mock<IQueuePoller<StorageBridgeFifoQueueClient>> _queuePollerMock = new();
    private readonly Mock<ILogger<StorageBridgeFifoQueueListener>> _loggerMock =
        new Mock<ILogger<StorageBridgeFifoQueueListener>>().EnableAllLogLevels();

    private StorageBridgeFifoQueueListener CreateSut(bool? disableQueueConsumer = null) =>
        new(_queuePollerMock.Object, _loggerMock.Object);

    [Fact]
    public async Task StartAsync_ShouldStartPoller_WhenNotDisabled()
    {
        var sut = CreateSut(disableQueueConsumer: false);

        await sut.StartAsync(TestContext.Current.CancellationToken);

        _queuePollerMock.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task StartAsync_ShouldStartPoller_WhenConfigurationNotSet()
    {
        var sut = CreateSut();

        await sut.StartAsync(TestContext.Current.CancellationToken);

        _queuePollerMock.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task StopAsync_ShouldStopPoller_WhenNotDisabled()
    {
        var sut = CreateSut(disableQueueConsumer: false);

        await sut.StopAsync(TestContext.Current.CancellationToken);

        _queuePollerMock.Verify(x => x.StopAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task StopAsync_ShouldSwallowTaskCanceledException_FromPoller()
    {
        _queuePollerMock
            .Setup(x => x.StopAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new TaskCanceledException());

        var sut = CreateSut(disableQueueConsumer: false);

        var act = async () => await sut.StopAsync(TestContext.Current.CancellationToken);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task StopAsync_ShouldSwallowObjectDisposedException_FromPoller()
    {
        _queuePollerMock
            .Setup(x => x.StopAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ObjectDisposedException("poller"));

        var sut = CreateSut(disableQueueConsumer: false);

        var act = async () => await sut.StopAsync(TestContext.Current.CancellationToken);

        await act.Should().NotThrowAsync();
    }
}