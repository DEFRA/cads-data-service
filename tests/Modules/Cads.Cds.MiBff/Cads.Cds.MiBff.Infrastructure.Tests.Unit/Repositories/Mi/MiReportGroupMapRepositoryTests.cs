using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiReportGroupMapRepositoryTests : IDisposable
{
    private readonly MiBffReadDbContext _context;
    private readonly IMiReportGroupMapRepository _repository;

    public MiReportGroupMapRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = TestDbContextHelper.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiReportGroupMapRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiReportGroupMapRepository(null!)));
    }

    [Fact]
    public async Task GetByGroupId_Found_ReturnsItems()
    {
        var result = await _repository.GetByGroupIdAsync(TestDataSeeder.GroupOps, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.GroupOps, result![0].GroupId);
        Assert.Equal(TestDataSeeder.ReportA, result![0].ReportId);
    }

    [Fact]
    public async Task GetByGroupId_NotFound_ReturnsEmpty()
    {
        var result = await _repository.GetByGroupIdAsync(Guid.Empty, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
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
        var result = await _repository.GetByKeyAsync(TestDataSeeder.GroupOps, TestDataSeeder.ReportA);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByKeyAsync(Guid.Empty, TestDataSeeder.PermissionView);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckExists_ForKnown_ReturnsTrue()
    {
        var result = await _repository.ExistsAsync(i => i.GroupId == TestDataSeeder.GroupOps && i.ReportId == TestDataSeeder.ReportA, TestContext.Current.CancellationToken);
        Assert.True(result);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.GroupId == Guid.Empty && i.ReportId == TestDataSeeder.ReportA, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.GroupId == TestDataSeeder.GroupOps && i.ReportId == TestDataSeeder.ReportA, orderBy: i => i.OrderByDescending(o => o.ReportId), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(TestDataSeeder.GroupOps, result![0].GroupId);
        Assert.Equal(TestDataSeeder.ReportA, result![0].ReportId);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.GroupId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.ReportId), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}