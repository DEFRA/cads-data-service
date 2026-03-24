using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiPermissionRepositoryTests : IDisposable
{
    private readonly MiBffReadDbContext _context;
    private readonly IMiPermissionRepository _repository;

    public MiPermissionRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextFactory.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiPermissionRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiPermissionRepository(null!)));
    }

    [Fact]
    public async Task GetByPermissionId_Found_ReturnsItem()
    {
        var result = await _repository.GetByPermissionIdAsync(TestDataSeeder.PermissionView, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(result.PermissionId, TestDataSeeder.PermissionView);
    }

    [Fact]
    public async Task GetByPermissionnId_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByPermissionIdAsync(Guid.Empty, TestContext.Current.CancellationToken);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.PermissionView);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByKeyAsync(Guid.Empty);
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
        var exists = await _repository.ExistsAsync(p => p.PermissionId == TestDataSeeder.PermissionView, TestContext.Current.CancellationToken);
        Assert.True(exists);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.PermissionId == Guid.Empty, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.PermissionId == TestDataSeeder.PermissionView, orderBy: i => i.OrderByDescending(o => o.PermissionKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.PermissionId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.PermissionKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}