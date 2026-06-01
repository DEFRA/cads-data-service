using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.Domain.Repositories;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.BulkLoad.Services;

public class S3SqlScriptExecutorServiceTests
{
    private readonly Mock<IServiceScopeFactory> _scopeFactory = new();
    private readonly Mock<IServiceScope> _scope = new();
    private readonly Mock<IServiceProvider> _provider = new();

    private readonly Mock<IStorageService<CadsInternalClient>> _storageService = new();
    private readonly Mock<IFileChecksumService> _checksumService = new();
    private readonly Mock<IDataSeedIngestionHistoryRepository> _historyRepo = new();
    private readonly Mock<ILogger<S3SqlScriptExecutorService>> _logger = new();

    // DbContext is not exercised in unit tests — DB-touching methods are ExcludeFromCodeCoverage
    private readonly StorageBridgeWriteDbContext _dbContext = null!;

    private const string TestPrefix = "sql-scripts/";
    private const string TestKey = "sql-scripts/insert_animals.sql";

    // -----------------------------------------------------------------------
    // ExecuteAsync — argument validation (fires before any I/O, fully testable)
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ExecuteAsync_ShouldThrow_WhenSourceKeyIsNullOrWhitespace(string? prefix)
    {
        var sut = CreateService();
        var job = new CreateS3SqlImportJobDto { SourceKey = prefix! };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            sut.ExecuteAsync(job, TestContext.Current.CancellationToken));
    }

    // -----------------------------------------------------------------------
    // ExecuteAsync — no keys found
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ExecuteAsync_ShouldReturnZero_WhenNoKeysFound()
    {
        _storageService
            .Setup(x => x.ListKeysAsync(TestPrefix, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await CreateService().ExecuteAsync(
            new CreateS3SqlImportJobDto { SourceKey = TestPrefix },
            TestContext.Current.CancellationToken);

        result.Should().Be(0);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldNotCallGetObject_WhenNoKeysFound()
    {
        _storageService
            .Setup(x => x.ListKeysAsync(TestPrefix, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        await CreateService().ExecuteAsync(
            new CreateS3SqlImportJobDto { SourceKey = TestPrefix },
            TestContext.Current.CancellationToken);

        _storageService.Verify(
            x => x.GetObjectResponseAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    // -----------------------------------------------------------------------
    // ExecuteAsync — empty/null S3 stream skips file (returns 0 successes)
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ExecuteAsync_ShouldReturnZero_WhenResponseStreamIsNull()
    {
        _storageService
            .Setup(x => x.ListKeysAsync(TestKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync([TestKey]);

        _storageService
            .Setup(x => x.GetObjectResponseAsync(TestKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetObjectResponse()); // ResponseStream is null

        var result = await CreateService().ExecuteAsync(
            new CreateS3SqlImportJobDto { SourceKey = TestKey },
            TestContext.Current.CancellationToken);

        result.Should().Be(0);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnZero_WhenFileContentIsWhitespace()
    {
        _storageService
            .Setup(x => x.ListKeysAsync(TestKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync([TestKey]);

        _storageService
            .Setup(x => x.GetObjectResponseAsync(TestKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetObjectResponse
            {
                ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes("   "))
            });

        var result = await CreateService().ExecuteAsync(
            new CreateS3SqlImportJobDto { SourceKey = TestKey },
            TestContext.Current.CancellationToken);

        result.Should().Be(0);
    }

    // -----------------------------------------------------------------------
    // ExecuteAsync — ListKeysAsync propagates exceptions
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ExecuteAsync_ShouldPropagate_WhenListKeysFails()
    {
        _storageService
            .Setup(x => x.ListKeysAsync(TestPrefix, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("S3 unreachable"));

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            CreateService().ExecuteAsync(
                new CreateS3SqlImportJobDto { SourceKey = TestPrefix },
                TestContext.Current.CancellationToken));
    }

    // -----------------------------------------------------------------------
    // Helpers
    // -----------------------------------------------------------------------


    private S3SqlScriptExecutorService CreateService()
    {
        _provider.Setup(x => x.GetService(typeof(StorageBridgeWriteDbContext)))
            .Returns(null!);

        _provider.Setup(x => x.GetService(typeof(IStorageService<CadsInternalClient>)))
            .Returns(_storageService);

        _provider.Setup(x => x.GetService(typeof(IFileChecksumService)))
            .Returns(_checksumService);

        _provider.Setup(x => x.GetService(typeof(IDataSeedIngestionHistoryRepository)))
            .Returns(_historyRepo);

        _scope.Setup(x => x.ServiceProvider).Returns(_provider.Object);

        _scopeFactory.Setup(x => x.CreateScope()).Returns(_scope.Object);

        return new S3SqlScriptExecutorService(
            _scopeFactory.Object,
            _logger.Object);
    }
}