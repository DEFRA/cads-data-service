using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using Cads.Cds.Ingester.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.Ingester.Infrastructure.Tests.Unit.Repositories;

public class DataSeedIngestionHistoryReadRepositoryTests : IDisposable
{
    private readonly IngesterReadDbContext _context;
    private readonly DataSeedIngestionHistoryReadRepository _repository;

    public DataSeedIngestionHistoryReadRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextFactory.CreateInMemoryDbContext<IngesterReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new DataSeedIngestionHistoryReadRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            Task.FromResult(new DataSeedIngestionHistoryReadRepository(null!)));

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