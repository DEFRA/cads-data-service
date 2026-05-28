using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.Ingester.Core.Domain.Entities;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using Cads.Cds.Ingester.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Ingester.Infrastructure.Tests.Unit.Repositories;

public class DataSeedIngestionHistoryWriteRepositoryTests : IDisposable
{
    private readonly IngesterWriteDbContext _context;
    private readonly DataSeedIngestionHistoryWriteRepository _repository;

    public DataSeedIngestionHistoryWriteRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextFactory.CreateInMemoryDbContext<IngesterWriteDbContext>(Guid.NewGuid().ToString());
        _repository = new DataSeedIngestionHistoryWriteRepository(_context);
    }

    [Fact]
    public async Task AddAsync_WithValidEntity_AddsEntityToContext()
    {
        var entity = new DataSeedIngestionHistory
        {
            Id = 1,
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
            Id = 1,
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
            Id = 1,
            FileName = "file3",
            AppliedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Checksum = "abc123"
        };

        await _context.DataSeedIngestionHistories.AddAsync(entity, TestContext.Current.CancellationToken);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        await _repository.Remove(entity, TestContext.Current.CancellationToken);
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
            Id = 1,
            FileName = "file4",
            AppliedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Checksum = "abc123"
        };

        await _context.DataSeedIngestionHistories.AddAsync(entity, TestContext.Current.CancellationToken);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        await _repository.Remove(entity, TestContext.Current.CancellationToken);

        var entry = _context.Entry(entity);

        Assert.Equal(EntityState.Deleted, entry.State);
    }

    [Fact]
    public async Task Remove_WithNullEntity_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _repository.Remove(null!, TestContext.Current.CancellationToken));

        Assert.NotNull(exception);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}