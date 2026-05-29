using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.Ingester.Infrastructure.Tests.Unit.Repositories;
using Cads.Cds.StorageBridge.Core.Domain.Entities;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Repositories;

public class DataSeedIngestionHistoryRepositoryTests : IDisposable
{
    private readonly StorageBridgeWriteDbContext _context;
    private readonly DataSeedIngestionHistoryRepository _repository;

    public DataSeedIngestionHistoryRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextFactory.CreateInMemoryDbContext<StorageBridgeWriteDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new DataSeedIngestionHistoryRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            Task.FromResult(new DataSeedIngestionHistoryRepository(null!)));

        Assert.NotNull(exception);
    }

    [Fact]
    public async Task AddAsync_WithValidEntity_AddsEntityToContext()
    {
        var entity = new DataSeedIngestionHistory
        {
            FileName = "file1",
            AppliedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Checksum = "abc123"
        };

        await _repository.AddAsync(entity, TestContext.Current.CancellationToken);

        var entry = _context.Entry(entity);
        Assert.Equal(EntityState.Added, entry.State);
    }

    [Fact]
    public async Task AddAsync_WithValidEntity_PersistsEntityAfterSaveChanges()
    {
        var entity = new DataSeedIngestionHistory
        {
            FileName = "file2",
            AppliedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Checksum = "abc123"
        };

        await _repository.AddAsync(entity, TestContext.Current.CancellationToken);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _context.DataSeedIngestionHistories
            .SingleOrDefaultAsync(x => x.Id == entity.Id, TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Equal(entity.FileName, result.FileName);
        Assert.Equal(entity.AppliedAt, result.AppliedAt);
        Assert.Equal(entity.Checksum, result.Checksum);
    }

    [Fact]
    public async Task AddAsync_WithNullEntity_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _repository.AddAsync(null!, TestContext.Current.CancellationToken));

        Assert.NotNull(exception);
    }
    [Fact]
    public async Task Remove_WithExistingEntity_RemovesEntityAndSavesChanges()
    {
        var entity = new DataSeedIngestionHistory
        {
            FileName = "file3",
            AppliedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Checksum = "abc123"
        };

        await _context.DataSeedIngestionHistories.AddAsync(entity, TestContext.Current.CancellationToken);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        await _repository.Remove(entity);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _context.DataSeedIngestionHistories
            .SingleOrDefaultAsync(x => x.Id == entity.Id, TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task Remove_WithExistingEntity_DetachesDeletedEntity()
    {
        var entity = new DataSeedIngestionHistory
        {
            FileName = "file4",
            AppliedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Checksum = "abc123"
        };

        await _context.DataSeedIngestionHistories.AddAsync(entity, TestContext.Current.CancellationToken);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        await _repository.Remove(entity);

        var entry = _context.Entry(entity);

        Assert.Equal(EntityState.Deleted, entry.State);
    }

    [Fact]
    public async Task Remove_WithNullEntity_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _repository.Remove(null!));

        Assert.NotNull(exception);
    }
    
        [Fact]
    public async Task GetById_Found_ReturnsItem()
    {
        var result = await _repository.GetByIdAsync(
            TestDataSeeder.FirstDataSeedIngestionHistoryId,
            TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.FirstDataSeedIngestionHistoryId, result.Id);
    }

    [Fact]
    public async Task GetById_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByIdAsync(0, TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByFileName_Found_ReturnsItem()
    {
        var result = await _repository.GetByFileNameAsync(
            TestDataSeeder.FirstDataSeedIngestionHistoryFileName,
            TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.FirstDataSeedIngestionHistoryFileName, result.FileName);
    }

    [Fact]
    public async Task GetByFileName_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByFileNameAsync(
            "unknown_seed_file",
            TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.FirstDataSeedIngestionHistoryId);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByKeyAsync(0L);

        Assert.Null(result);
    }

    [Fact]
    public async Task List_ReturnsItems()
    {
        var items = await _repository.ListAsync(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(items);
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public async Task CheckExists_ForKnown_ReturnsTrue()
    {
        var exists = await _repository.ExistsAsync(
            x => x.Id == TestDataSeeder.FirstDataSeedIngestionHistoryId,
            TestContext.Current.CancellationToken);

        Assert.True(exists);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(
            x => x.Id == 0,
            TestContext.Current.CancellationToken);

        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(
            x => x.Id == TestDataSeeder.FirstDataSeedIngestionHistoryId,
            orderBy: x => x.OrderByDescending(o => o.FileName),
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task Find_ForUnknown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(
            x => x.Id == 0,
            orderBy: x => x.OrderByDescending(o => o.FileName),
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}