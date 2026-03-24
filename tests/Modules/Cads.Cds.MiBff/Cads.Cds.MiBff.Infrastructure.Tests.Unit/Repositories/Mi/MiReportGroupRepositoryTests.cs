using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiReportGroupRepositoryTests : IDisposable
{
    private readonly MiBffReadDbContext _context;
    private readonly IMiReportGroupRepository _repository;

    public MiReportGroupRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextFactory.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiReportGroupRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiReportGroupRepository(null!)));
    }

    [Fact]
    public async Task GetByGroupId_Found_ReturnsItem()
    {
        var result = await _repository.GetByGroupIdAsync(TestDataSeeder.GroupOps, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.GroupOps, result!.GroupId);
    }

    [Fact]
    public async Task GetByGroupId_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByGroupIdAsync(Guid.Empty, TestContext.Current.CancellationToken);
        Assert.Null(result);
    }

    [Fact]
    public async Task List_ReturnsItems()
    {
        var result = await _repository.ListAsync(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.GroupOps);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.PermissionView);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsTrue()
    {
        var exists = await _repository.ExistsAsync(g => g.GroupId == TestDataSeeder.GroupOps, CancellationToken.None);
        Assert.True(exists);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.GroupId == Guid.Empty, TestContext.Current.CancellationToken);
        Assert.False(result);
    }


    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.GroupId == TestDataSeeder.GroupOps, orderBy: i => i.OrderByDescending(o => o.GroupKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.GroupId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.GroupKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}