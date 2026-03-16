using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiRoleRepositoryTests : IDisposable
{
    private readonly CadsReadOnlyDbContext _context;
    private readonly IMiRoleRepository _repository;

    public MiRoleRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextHelper.GetInMemoryDbContext<CadsReadOnlyDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiRoleRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiRoleRepository(null!)));
    }

    [Fact]
    public async Task GetByRoleId_Found_ReturnsItem()
    {
        var result = await _repository.GetByRoleIdAsync(TestDataSeeder.RoleAdmin, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.RoleAdmin, result!.RoleId);
    }

    [Fact]
    public async Task GetByReportId_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByRoleIdAsync(Guid.Empty, CancellationToken.None);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAll_ReturnsItems()
    {
        var roles = await _repository.GetAllAsync(CancellationToken.None);
        Assert.NotNull(roles);
        Assert.Equal(2, roles.Count());
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.RoleAdmin);
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
        var result = await _repository.ExistsAsync(i => i.RoleId == TestDataSeeder.RoleAdmin, TestContext.Current.CancellationToken);
        Assert.True(result);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.RoleId == Guid.Empty, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.RoleId == TestDataSeeder.RoleAdmin, orderBy: i => i.OrderByDescending(o => o.CreatedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.RoleId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.CreatedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}