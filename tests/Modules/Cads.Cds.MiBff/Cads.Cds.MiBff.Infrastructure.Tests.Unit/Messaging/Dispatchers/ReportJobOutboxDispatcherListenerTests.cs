using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Messaging.Dispatchers;

public class ReportJobOutboxDispatcherListenerTests
{
    private readonly Mock<IOutboxDispatcher<MiBffFifoQueueClient>> _dispatcherMock = new();
    private readonly Mock<ILogger<ReportJobOutboxDispatcherListener>> _loggerMock =
        new Mock<ILogger<ReportJobOutboxDispatcherListener>>().EnableAllLogLevels();

    private ReportJobOutboxDispatcherListener CreateSut() =>
        new(_dispatcherMock.Object, _loggerMock.Object);

    [Fact]
    public async Task StartAsync_ShouldStartDispatcher()
    {
        var sut = CreateSut();

        await sut.StartAsync(TestContext.Current.CancellationToken);

        _dispatcherMock.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task StartAsync_ShouldPropagateException_FromDispatcher()
    {
        _dispatcherMock
            .Setup(x => x.StartAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var sut = CreateSut();

        var act = () => sut.StartAsync(TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("boom");
    }

    [Fact]
    public async Task StopAsync_ShouldStopDispatcher()
    {
        var sut = CreateSut();

        await sut.StopAsync(TestContext.Current.CancellationToken);

        _dispatcherMock.Verify(x => x.StopAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task StopAsync_ShouldSwallowTaskCanceledException_FromDispatcher()
    {
        _dispatcherMock
            .Setup(x => x.StopAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new TaskCanceledException());

        var sut = CreateSut();

        var act = () => sut.StopAsync(TestContext.Current.CancellationToken);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task StopAsync_ShouldPropagateOtherExceptions_FromDispatcher()
    {
        _dispatcherMock
            .Setup(x => x.StopAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var sut = CreateSut();

        var act = () => sut.StopAsync(TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("boom");
    }
}