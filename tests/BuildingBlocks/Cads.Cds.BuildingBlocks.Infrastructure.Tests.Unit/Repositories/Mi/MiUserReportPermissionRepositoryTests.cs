using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiUserReportPermissionRepositoryTests : IDisposable
{
    private readonly CadsReadOnlyDbContext _context;
    private readonly IMiUserReportPermissionRepository _repository;

    public MiUserReportPermissionRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextHelper.GetInMemoryDbContext<CadsReadOnlyDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiUserReportPermissionRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiUserReportPermissionRepository(null!)));
    }

    [Fact]
    public async Task GetByUserId_Found_ReturnsItems()
    {
        var result = await _repository.GetByUserIdAsync(TestDataSeeder.User1, CancellationToken.None);
        Assert.NotNull(result);
        Assert.True(result.Any());
        Assert.Equal(TestDataSeeder.User1, result.First()!.User.UserId);
        Assert.Equal(TestDataSeeder.PermissionView, result.First()!.Permission.PermissionId);
    }

    [Fact]
    public async Task GetByUserId_NotFound_ReturnsEmpty()
    {
        var result = await _repository.GetByUserIdAsync(Guid.Empty, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAll_ReturnsItems()
    {
        var result = await _repository.GetAllAsync(CancellationToken.None);
        Assert.NotNull(result);
        Assert.True(result.Any());
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.User1, TestDataSeeder.ReportA, TestDataSeeder.PermissionView);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.User1, result.User.UserId);
        Assert.Equal(TestDataSeeder.PermissionView, result.Permission.PermissionId);
    }

    [Fact]
    public async Task GetByKey_NotFound_Returns_Null()
    {
        var result = await _repository.GetByKeyAsync(Guid.Empty, TestDataSeeder.ReportA, TestDataSeeder.PermissionView);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckExists_ForKnown_ReturnsTrue()
    {
        var result = await _repository.ExistsAsync(i => i.UserId == TestDataSeeder.User1 && i.ReportId == TestDataSeeder.ReportA && i.PermissionId == TestDataSeeder.PermissionView, TestContext.Current.CancellationToken);
        Assert.True(result);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.UserId == Guid.Empty, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.UserId == TestDataSeeder.User1, orderBy: i => i.OrderByDescending(o => o.GrantedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.User1, result.First()!.User.UserId);
        Assert.Equal(TestDataSeeder.PermissionView, result.First()!.Permission.PermissionId);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.UserId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.GrantedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}