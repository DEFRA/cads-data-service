using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.BulkLoad.Services;

public class S3CsvBulkLoadBackgroundServiceTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCallExecuteAsync()
    {
        var ctx = new S3CsvBulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        var job = new CreateS3CsvBulkLoadJobDto { JobId = Guid.NewGuid() };

        await S3CsvBulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service);

        ctx.CopyService.Verify(s => s.ExecuteAsync(job, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldLogError_WhenExceptionThrown()
    {
        var ctx = new S3CsvBulkLoadBackgroundServiceTestContext();

        ctx.CopyService
            .Setup(s => s.ExecuteAsync(It.IsAny<CreateS3CsvBulkLoadJobDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var service = ctx.CreateService();

        await S3CsvBulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service);

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
        var ctx = new S3CsvBulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        await ctx.Channel.Writer.WriteAsync(new CreateS3CsvBulkLoadJobDto(), TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3CsvBulkLoadJobDto(), TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3CsvBulkLoadJobDto(), TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

        await service.StartAsync(CancellationToken.None);

        var executeTask = S3CsvBulkLoadBackgroundServiceTestContext.GetExecuteTask(service);
        await executeTask;

        await service.StopAsync(CancellationToken.None);

        ctx.CopyService.Verify(
            s => s.ExecuteAsync(It.IsAny<CreateS3CsvBulkLoadJobDto>(), It.IsAny<CancellationToken>()),
            Times.Exactly(3));
    }

    public class S3CsvBulkLoadBackgroundServiceTestContext
    {
        public Mock<ILogger<S3CsvBulkLoadBackgroundService>> Logger { get; } = new();
        public Mock<IS3ToPostgresCopyService> CopyService { get; } = new();

        public Channel<CreateS3CsvBulkLoadJobDto> Channel { get; } =
            System.Threading.Channels.Channel.CreateUnbounded<CreateS3CsvBulkLoadJobDto>();

        public S3CsvBulkLoadBackgroundService CreateService()
        {
            return new S3CsvBulkLoadBackgroundService(Channel, Logger.Object, CopyService.Object);
        }

        public static Task InvokeProcessJobAsync(
            S3CsvBulkLoadBackgroundService service)
        {
            var method = typeof(S3CsvBulkLoadBackgroundService)
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