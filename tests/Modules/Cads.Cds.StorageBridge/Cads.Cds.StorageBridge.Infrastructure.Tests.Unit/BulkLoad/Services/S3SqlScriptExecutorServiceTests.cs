using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Core.Domain.Repositories;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.BulkLoad.Services;

public class S3SqlScriptExecutorServiceTests
{
    private readonly Mock<IStorageReader<CadsInternalClient>> _storageReader = new();
    private readonly Mock<IS3ClientFactory> _s3ClientFactory = new();
    private readonly Mock<IFileChecksumService> _checksumService = new();
    private readonly Mock<IDataSeedIngestionHistoryRepository> _historyRepo = new();
    private readonly Mock<ILogger<S3SqlScriptExecutorService>> _logger = new();

    // DbContext is not exercised in unit tests — pass null; DB-touching methods are ExcludeFromCodeCoverage
    private readonly StorageBridgeWriteDbContext _dbContext = null!;

    private const string TestPrefix = "sql-scripts/";
    private const string TestKey = "sql-scripts/insert_animals.sql";
    private const string TestSql = "INSERT INTO animals (id, name) VALUES (1, 'Cow');";
    private const string TestChecksum = "abc123def456";

    // -----------------------------------------------------------------------
    // ExecuteAsync — argument validation
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ExecuteAsync_ShouldThrow_WhenSourceKeyPrefixIsNullOrWhitespace(string? prefix)
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
        _storageReader
            .Setup(x => x.ListKeysAsync(TestPrefix, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var sut = CreateService();
        var job = new CreateS3SqlImportJobDto { SourceKey = TestPrefix };

        var result = await sut.ExecuteAsync(job, TestContext.Current.CancellationToken);

        result.Should().Be(0);
    }

    // -----------------------------------------------------------------------
    // ReadSqlFromS3Async — null response stream returns empty string
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmpty_WhenResponseStreamIsNull()
    {
        _storageReader
            .Setup(x => x.GetObjectResponseAsync(TestKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetObjectResponse());
        var sut = CreateService();
        var result = await sut.ExecuteAsync(new CreateS3SqlImportJobDto
        {
            JobId = Guid.NewGuid(),
            SourceKey = TestKey,
        }, TestContext.Current.CancellationToken);

        result.Should().Be(0);
    }

    // -----------------------------------------------------------------------
    // Helpers
    // -----------------------------------------------------------------------

    private S3SqlScriptExecutorService CreateService() =>
        new(
            _dbContext,
            _storageReader.Object,
            _s3ClientFactory.Object,
            _checksumService.Object,
            _historyRepo.Object,
            _logger.Object);
}