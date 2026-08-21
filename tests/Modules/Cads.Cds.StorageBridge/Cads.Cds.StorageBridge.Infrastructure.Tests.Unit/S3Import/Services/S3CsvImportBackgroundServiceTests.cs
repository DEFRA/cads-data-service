using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Services;

public class S3CsvImportBackgroundServiceTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCallExecuteAsync()
    {
        var ctx = new S3CsvBulkLoadBackgroundServiceTestContext();
        var service = ctx.CreateService();

        var job = new CreateS3CsvImportJobDto { JobId = Guid.NewGuid(), FileImportId = 1 };

        await ctx.Channel.Writer.WriteAsync(job, TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

        await S3CsvBulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service);

        ctx.CopyService.Verify(s => s.ExecuteAsync(job, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldLogError_WhenExceptionThrown()
    {
        var ctx = new S3CsvBulkLoadBackgroundServiceTestContext();

        ctx.CopyService
            .Setup(s => s.ExecuteAsync(It.IsAny<CreateS3CsvImportJobDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var service = ctx.CreateService();

        await ctx.Channel.Writer.WriteAsync(new CreateS3CsvImportJobDto { JobId = Guid.NewGuid(), FileImportId = 1 }, TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

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

        await ctx.Channel.Writer.WriteAsync(new CreateS3CsvImportJobDto { JobId = Guid.NewGuid(), FileImportId = 1 }, TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3CsvImportJobDto { JobId = Guid.NewGuid(), FileImportId = 2 }, TestContext.Current.CancellationToken);
        await ctx.Channel.Writer.WriteAsync(new CreateS3CsvImportJobDto { JobId = Guid.NewGuid(), FileImportId = 3 }, TestContext.Current.CancellationToken);
        ctx.Channel.Writer.Complete();

        await service.StartAsync(CancellationToken.None);

        var executeTask = S3CsvBulkLoadBackgroundServiceTestContext.GetExecuteTask(service);
        await executeTask;

        await service.StopAsync(CancellationToken.None);

        ctx.CopyService.Verify(
            s => s.ExecuteAsync(It.IsAny<CreateS3CsvImportJobDto>(), It.IsAny<CancellationToken>()),
            Times.Exactly(3));
    }

    [Fact]
    public async Task ProcessJob_WhenCancelledDuringShutdown_PersistsFailedStatusWithNonCancelledToken()
    {
        var ctx = new S3CsvBulkLoadBackgroundServiceTestContext();

        // Simulate the host shutdown token being cancelled while the long-running
        // import is in progress.
        using var stoppingCts = new CancellationTokenSource();
        await stoppingCts.CancelAsync();

        ctx.CopyService
            .Setup(s => s.ExecuteAsync(It.IsAny<CreateS3CsvImportJobDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        var service = ctx.CreateService();
        var job = new CreateS3CsvImportJobDto { JobId = Guid.NewGuid(), FileImportId = 1 };

        await S3CsvBulkLoadBackgroundServiceTestContext.InvokeProcessJobAsync(service, job, stoppingCts.Token);

        // The failure status must be persisted using a token that is NOT the cancelled
        // shutdown token, otherwise the status update fails and the import is stranded.
        ctx.DbContext.Verify(
            db => db.SaveChangesAsync(It.Is<CancellationToken>(t => !t.IsCancellationRequested)),
            Times.Once);
    }

    public class S3CsvBulkLoadBackgroundServiceTestContext
    {
        private readonly Mock<IServiceScopeFactory> _scopeFactory = new();
        private readonly Mock<IServiceScope> _scope = new();
        private readonly Mock<IServiceProvider> _provider = new();
        private readonly Mock<IStorageBridgeFileImportRepository> _fileImportRepository = new();

        // DbContext is mocked so SaveChangesAsync can be controlled/verified.
        public Mock<StorageBridgeWriteDbContext> DbContext { get; } =
            new(new DbContextOptions<StorageBridgeWriteDbContext>());

        public Mock<ILogger<S3CsvImportBackgroundService>> Logger { get; } = new();
        public Mock<IS3ToPostgresCopyService> CopyService { get; } = new();

        public Channel<CreateS3CsvImportJobDto> Channel { get; } =
            System.Threading.Channels.Channel.CreateUnbounded<CreateS3CsvImportJobDto>();

        // -----------------------------------------------------------------------
        // Helpers
        // -----------------------------------------------------------------------

        public S3CsvImportBackgroundService CreateService()
        {
            _scopeFactory.Setup(x => x.CreateScope())
                .Returns(_scope.Object);
            _scope.Setup(x => x.ServiceProvider)
                .Returns(_provider.Object);
            _provider.Setup(x => x.GetService(typeof(StorageBridgeWriteDbContext)))
                .Returns(DbContext.Object);
            _provider.Setup(x => x.GetService(typeof(IStorageBridgeFileImportRepository)))
                .Returns(_fileImportRepository.Object);
            _fileImportRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FileImport());

            DbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            Logger.Setup(l => l.IsEnabled(LogLevel.Error))
                .Returns(true);

            return new S3CsvImportBackgroundService(Channel, _scopeFactory.Object, Logger.Object, CopyService.Object);
        }

        public static Task InvokeProcessJobAsync(
            S3CsvImportBackgroundService service)
        {
            var method = typeof(S3CsvImportBackgroundService)
                .GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            return (Task)method!.Invoke(service, [CancellationToken.None])!;
        }

        public static Task InvokeProcessJobAsync(
            S3CsvImportBackgroundService service,
            CreateS3CsvImportJobDto job,
            CancellationToken cancellationToken)
        {
            var method = typeof(S3ImportBackgroundService<CreateS3CsvImportJobDto>)
                .GetMethod("ProcessJobAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            return (Task)method!.Invoke(service, [job, new SemaphoreSlim(1), cancellationToken])!;
        }

        public static Task GetExecuteTask(BackgroundService service)
        {
            var field = typeof(BackgroundService)
                .GetField("_executeTask", BindingFlags.Instance | BindingFlags.NonPublic);

            return (Task)field!.GetValue(service)!;
        }
    }
}