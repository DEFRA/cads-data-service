using Cads.Cds.SystemAdmin.Application.Promotion.Factories;
using Cads.Cds.SystemAdmin.Application.Promotion.Jobs;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Channels;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Promotion;

public class PromotionJobBackgroundServiceTests
{
    private static readonly TimeSpan s_timeout = TimeSpan.FromSeconds(5);

    private readonly Mock<IPromotionJobProcessorStrategyFactory> _factoryMock = new();
    private readonly Mock<IPromotionJobProcessorStrategy> _strategyMock = new();

    private (PromotionJobQueue Queue, PromotionJobBackgroundService Sut) CreateSut(bool ctsBulkEnabled = true)
    {
        _factoryMock
            .Setup(x => x.GetStrategy(PromotionType.CtsBulk))
            .Returns(_strategyMock.Object);

        var services = new ServiceCollection();
        services.AddTransient(_ => _factoryMock.Object);
        var provider = services.BuildServiceProvider();

        var queue = new PromotionJobQueue();
        var sut = new PromotionJobBackgroundService(
            queue,
            services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>(),
            NullLogger<PromotionJobBackgroundService>.Instance);

        return (queue, sut);
    }

    private static bool WaitForSlotRelease(PromotionJobQueue queue) =>
        SpinWait.SpinUntil(
            () => queue.TryEnqueue(PromotionType.CtsBulk, "release-probe", null, "probe"),
            s_timeout);

    [Fact]
    public async Task GivenQueuedJob_WhenProcessed_ShouldCallStrategyAndReleaseQueue()
    {
        var processed = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        _strategyMock
            .Setup(x => x.PrepareAsync(It.IsAny<PromotionJob>(), It.IsAny<CancellationToken>()))
            .Callback<PromotionJob, CancellationToken>((_, _) => processed.TrySetResult())
            .ReturnsAsync(42L);

        var (queue, sut) = CreateSut();
        await sut.StartAsync(TestContext.Current.CancellationToken);

        try
        {
            queue.TryEnqueue(PromotionType.CtsBulk, "corr-1", false, "user").Should().BeTrue();

            await processed.Task.WaitAsync(s_timeout, TestContext.Current.CancellationToken);

            // The slot is released once the job has finished, so a new job can be queued.
            WaitForSlotRelease(queue).Should().BeTrue();

            _strategyMock.Verify(x => x.PrepareAsync(
                It.Is<PromotionJob>(j =>
                    j.PromotionType == PromotionType.CtsBulk &&
                    j.CorrelationId == "corr-1" &&
                    j.DeleteSource == false &&
                    j.RequestedBy == "user"),
                It.IsAny<CancellationToken>()), Times.Once);
        }
        finally
        {
            await sut.StopAsync(CancellationToken.None);
        }
    }

    [Fact]
    public async Task GivenPrepareThrows_WhenProcessed_ShouldReleaseQueueAndKeepRunning()
    {
        _strategyMock
            .Setup(x => x.PrepareAsync(It.IsAny<PromotionJob>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("An unfinished CTS parallel import run already exists"));

        var (queue, sut) = CreateSut();
        await sut.StartAsync(TestContext.Current.CancellationToken);

        try
        {
            queue.TryEnqueue(PromotionType.CtsBulk, "corr-1", null, "user").Should().BeTrue();

            // Slot released after the failure...
            WaitForSlotRelease(queue).Should().BeTrue();

            // ...and the service is still consuming: the probe job is also processed.
            SpinWait.SpinUntil(() => _strategyMock.Invocations.Count >= 2, s_timeout)
                .Should().BeTrue();
        }
        finally
        {
            await sut.StopAsync(CancellationToken.None);
        }
    }
}