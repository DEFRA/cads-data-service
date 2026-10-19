using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.DbAdmin.Services;

public class CtsImportPrepareBackgroundServiceTests
{
    private readonly Mock<ICtsImportPrepareService> _prepareService = new();

    private (CtsImportPrepareJobQueue Queue, CtsImportPrepareBackgroundService Sut) CreateSut()
    {
        var services = new ServiceCollection();
        services.AddScoped(_ => _prepareService.Object);

        var queue = new CtsImportPrepareJobQueue();
        var sut = new CtsImportPrepareBackgroundService(
            queue,
            services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>(),
            NullLogger<CtsImportPrepareBackgroundService>.Instance);

        return (queue, sut);
    }

    [Fact]
    public async Task GivenQueuedJob_WhenProcessed_ShouldCallPrepareAndReleaseQueue()
    {
        _prepareService
            .Setup(x => x.PrepareAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(42L);

        var (queue, sut) = CreateSut();
        await sut.StartAsync(TestContext.Current.CancellationToken);

        try
        {
            queue.TryEnqueue("user").Should().BeTrue();

            // The slot is released once the job has finished, so a new job can be queued.
            SpinWait.SpinUntil(() => queue.TryEnqueue("next"), TimeSpan.FromSeconds(5))
                .Should().BeTrue();

            _prepareService.Verify(x => x.PrepareAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
        finally
        {
            await sut.StopAsync(CancellationToken.None);
        }
    }

    [Fact]
    public async Task GivenPrepareThrows_WhenProcessed_ShouldReleaseQueueAndKeepRunning()
    {
        _prepareService
            .Setup(x => x.PrepareAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("An unfinished CTS parallel import run already exists"));

        var (queue, sut) = CreateSut();
        await sut.StartAsync(TestContext.Current.CancellationToken);

        try
        {
            queue.TryEnqueue("user").Should().BeTrue();

            SpinWait.SpinUntil(() => queue.TryEnqueue("next"), TimeSpan.FromSeconds(5))
                .Should().BeTrue();
        }
        finally
        {
            await sut.StopAsync(CancellationToken.None);
        }
    }
}