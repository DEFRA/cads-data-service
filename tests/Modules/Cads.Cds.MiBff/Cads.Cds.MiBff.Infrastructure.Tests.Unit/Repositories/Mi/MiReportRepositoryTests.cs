using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiReportRepositoryTests : IDisposable
{
    private readonly MiBffReadDbContext _context;
    private readonly IMiReportRepository _repository;

    public MiReportRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = TestDbContextHelper.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
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
        var result = await _repository.GetByReportIdAsync(TestDataSeeder.ReportA, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.ReportA, result!.ReportId);
    }

    [Fact]
    public async Task GetByReportId_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByReportIdAsync(Guid.Empty, TestContext.Current.CancellationToken);
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