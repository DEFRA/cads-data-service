using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiReportRepositoryTests : IDisposable
{
    private readonly CadsReadOnlyDbContext _context;
    private readonly IMiReportRepository _repository;

    public MiReportRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextHelper.GetInMemoryDbContext<CadsReadOnlyDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiReportRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiReportRepository(null!)));
    }

    [Fact]
    public async Task GetByReportId_Found_ReturnsItem()
    {
        var result = await _repository.GetByReportIdAsync(TestDataSeeder.ReportA, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.ReportA, result!.ReportId);
    }

    [Fact]
    public async Task GetByReportId_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByReportIdAsync(Guid.Empty, CancellationToken.None);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAll_ReturnsItems()
    {
        var items = await _repository.GetAllAsync(CancellationToken.None);
        Assert.NotNull(items);
        Assert.Equal(2, items.Count());
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.ReportA);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByKeyAsync(Guid.Empty);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckExists_ForKnown_ReturnsTrue()
    {
        var result = await _repository.ExistsAsync(i => i.ReportId == TestDataSeeder.ReportA, TestContext.Current.CancellationToken);
        Assert.True(result);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.ReportId == Guid.Empty, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.ReportId == TestDataSeeder.ReportA, orderBy: i => i.OrderByDescending(o => o.CreatedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.ReportId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.CreatedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}