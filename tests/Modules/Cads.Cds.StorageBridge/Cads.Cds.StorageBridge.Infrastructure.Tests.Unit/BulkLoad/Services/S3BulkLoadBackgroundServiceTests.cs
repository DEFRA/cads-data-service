using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.BulkLoad.Services;

public class S3BulkLoadBackgroundServiceTests
{
    [Fact]
    public async Task ProcessJobAsync_ShouldCallExecuteAsync()
    {
        var ctx = new S3BulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        var job = new CreateS3BulkLoadJobDto { JobId = Guid.NewGuid() };
        var semaphore = new SemaphoreSlim(1);

        await S3BulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service, job, semaphore);

        ctx.CopyService.Verify(s => s.ExecuteAsync(job, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ProcessJobAsync_ShouldLogError_WhenExceptionThrown()
    {
        var ctx = new S3BulkLoadBackgroundServiceTestContext();

        ctx.CopyService
            .Setup(s => s.ExecuteAsync(It.IsAny<CreateS3BulkLoadJobDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var service = ctx.CreateService();

        var job = new CreateS3BulkLoadJobDto { JobId = Guid.NewGuid() };
        var semaphore = new SemaphoreSlim(1);

        await S3BulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service, job, semaphore);

        ctx.Logger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldProcessAllJobs()
    {
        var ctx = new S3BulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        await ctx.Channel.Writer.WriteAsync(new CreateS3BulkLoadJobDto(), TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3BulkLoadJobDto(), TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3BulkLoadJobDto(), TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

        await service.StartAsync(CancellationToken.None);

        var executeTask = S3BulkLoadBackgroundServiceTestContext.GetExecuteTask(service);
        await executeTask;

        await service.StopAsync(CancellationToken.None);

        ctx.CopyService.Verify(
            s => s.ExecuteAsync(It.IsAny<CreateS3BulkLoadJobDto>(), It.IsAny<CancellationToken>()),
            Times.Exactly(3));
    }

    public class S3BulkLoadBackgroundServiceTestContext
    {
        public Mock<IServiceScopeFactory> ScopeFactory { get; } = new();
        public Mock<IServiceScope> Scope { get; } = new();
        public Mock<IServiceProvider> Provider { get; } = new();
        public Mock<ILogger<S3BulkLoadBackgroundService>> Logger { get; } = new();
        public Mock<IS3ToPostgresCopyService> CopyService { get; } = new();

        public Channel<CreateS3BulkLoadJobDto> Channel { get; } =
            System.Threading.Channels.Channel.CreateUnbounded<CreateS3BulkLoadJobDto>();

        public S3BulkLoadBackgroundService CreateService()
        {
            ScopeFactory.Setup(f => f.CreateScope()).Returns(Scope.Object);
            Scope.Setup(s => s.ServiceProvider).Returns(Provider.Object);

            Provider.Setup(p => p.GetService(typeof(IS3ToPostgresCopyService)))
                    .Returns(CopyService.Object);

            return new S3BulkLoadBackgroundService(Channel, ScopeFactory.Object, Logger.Object);
        }

        public static Task InvokeProcessJobAsync(
            S3BulkLoadBackgroundService service,
            CreateS3BulkLoadJobDto job,
            SemaphoreSlim semaphore)
        {
            var method = typeof(S3BulkLoadBackgroundService)
                .GetMethod("ProcessJobAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            return (Task)method!.Invoke(service, [job, semaphore, CancellationToken.None])!;
        }

        public static Task GetExecuteTask(BackgroundService service)
        {
            var field = typeof(BackgroundService)
                .GetField("_executeTask", BindingFlags.Instance | BindingFlags.NonPublic);

            return (Task)field!.GetValue(service)!;
        }
    }
}