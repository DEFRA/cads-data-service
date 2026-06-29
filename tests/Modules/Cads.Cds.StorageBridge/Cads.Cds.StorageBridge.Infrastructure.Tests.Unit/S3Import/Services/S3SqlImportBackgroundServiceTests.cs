using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
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

    public class S3SqlBulkLoadBackgroundServiceTestContext
    {
        public Mock<ILogger<S3SqlImportBackgroundService>> Logger { get; } = new();
        public Mock<IS3SqlScriptExecutorService> CopyService { get; } = new();

        public Channel<CreateS3SqlImportJobDto> Channel { get; } =
            System.Threading.Channels.Channel.CreateUnbounded<CreateS3SqlImportJobDto>();

        public S3SqlImportBackgroundService CreateService()
        {
            return new S3SqlImportBackgroundService(Channel, Logger.Object, CopyService.Object);
        }

        public static Task InvokeProcessJobAsync(
            S3SqlImportBackgroundService service)
        {
            var method = typeof(S3SqlImportBackgroundService)
                .GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            return (Task)method!.Invoke(service, [CancellationToken.None])!;
        }

        public static Task GetExecuteTask(BackgroundService service)
        {
            var field = typeof(BackgroundService)
                .GetField("_executeTask", BindingFlags.Instance | BindingFlags.NonPublic);

            return (Task)field!.GetValue(service)!;
        }
    }
}