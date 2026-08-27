using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Services;

public class S3SqlImportBackgroundServiceTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCallExecuteAsync()
    {
        var ctx = new S3SqlBulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        var job = new CreateS3SqlImportJobDto { JobId = Guid.NewGuid() };

        await ctx.Channel.Writer.WriteAsync(job, TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

        await S3SqlBulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service);

        ctx.CopyService.Verify(s => s.ExecuteAsync(job, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldLogError_WhenExceptionThrown()
    {
        var ctx = new S3SqlBulkLoadBackgroundServiceTestContext();

        ctx.CopyService
            .Setup(s => s.ExecuteAsync(It.IsAny<CreateS3SqlImportJobDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var service = ctx.CreateService();

        await ctx.Channel.Writer.WriteAsync(new CreateS3SqlImportJobDto(), TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

        await S3SqlBulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service);

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
        var ctx = new S3SqlBulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        await ctx.Channel.Writer.WriteAsync(new CreateS3SqlImportJobDto(), TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3SqlImportJobDto(), TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3SqlImportJobDto(), TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

        await service.StartAsync(CancellationToken.None);

        var executeTask = S3SqlBulkLoadBackgroundServiceTestContext.GetExecuteTask(service);
        await executeTask;

        await service.StopAsync(CancellationToken.None);

        ctx.CopyService.Verify(
            s => s.ExecuteAsync(It.IsAny<CreateS3SqlImportJobDto>(), It.IsAny<CancellationToken>()),
            Times.Exactly(3));
    }

    [Fact]
    public async Task ExecuteAsync_WhenStoppingTokenCancelled_CompletesGracefullyWithoutThrowing()
    {
        var ctx = new S3SqlBulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        using var stoppingCts = new CancellationTokenSource();

        // Do NOT complete the channel so the reader is blocked awaiting new jobs,
        // mirroring a live service when the host begins shutting down.
        var executeTask = S3SqlBulkLoadBackgroundServiceTestContext.InvokeExecuteAsync(service, stoppingCts.Token);

        await stoppingCts.CancelAsync();

        // If the graceful-shutdown catch block were missing, the cancellation would
        // propagate and this task would fault/cancel. Completing normally proves the
        // OperationCanceledException is swallowed on shutdown.
        await executeTask;

        Assert.True(executeTask.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task ExecuteAsync_WhenStoppingTokenCancelled_StillDrainsInFlightJobs()
    {
        var ctx = new S3SqlBulkLoadBackgroundServiceTestContext();

        var jobStarted = new TaskCompletionSource();
        var releaseJob = new TaskCompletionSource<long>();

        // Block the in-flight job until we've cancelled the stopping token, so we can
        // prove that the finally/WhenAll still awaits it to completion.
        ctx.CopyService
            .Setup(s => s.ExecuteAsync(It.IsAny<CreateS3SqlImportJobDto>(), It.IsAny<CancellationToken>()))
            .Callback(() => jobStarted.TrySetResult())
            .Returns(releaseJob.Task);

        var service = ctx.CreateService();

        using var stoppingCts = new CancellationTokenSource();

        await ctx.Channel.Writer.WriteAsync(new CreateS3SqlImportJobDto(), TestContext.Current.CancellationToken);

        var executeTask = S3SqlBulkLoadBackgroundServiceTestContext.InvokeExecuteAsync(service, stoppingCts.Token);

        // Wait until the job is actually running, then request shutdown.
        await jobStarted.Task;
        await stoppingCts.CancelAsync();

        // The service must not complete until the in-flight job is drained.
        Assert.False(executeTask.IsCompleted);

        releaseJob.SetResult(0);

        await executeTask;

        Assert.True(executeTask.IsCompletedSuccessfully);
        ctx.CopyService.Verify(
            s => s.ExecuteAsync(It.IsAny<CreateS3SqlImportJobDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    public class S3SqlBulkLoadBackgroundServiceTestContext
    {
        public Mock<ILogger<S3SqlImportBackgroundService>> Logger { get; } = new();
        public Mock<IS3SqlScriptExecutorService> CopyService { get; } = new();

        public Channel<CreateS3SqlImportJobDto> Channel { get; } =
            System.Threading.Channels.Channel.CreateUnbounded<CreateS3SqlImportJobDto>();

        public S3SqlImportBackgroundService CreateService()
        {
            Logger.Setup(l => l.IsEnabled(LogLevel.Error))
                .Returns(true);

            return new S3SqlImportBackgroundService(Channel, Logger.Object, CopyService.Object);
        }

        public static Task InvokeProcessJobAsync(
            S3SqlImportBackgroundService service)
        {
            return InvokeExecuteAsync(service, CancellationToken.None);
        }

        public static Task InvokeExecuteAsync(
            S3SqlImportBackgroundService service,
            CancellationToken stoppingToken)
        {
            var method = typeof(S3SqlImportBackgroundService)
                .GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            return (Task)method!.Invoke(service, [stoppingToken])!;
        }

        public static Task GetExecuteTask(BackgroundService service)
        {
            var field = typeof(BackgroundService)
                .GetField("_executeTask", BindingFlags.Instance | BindingFlags.NonPublic);

            return (Task)field!.GetValue(service)!;
        }
    }
}