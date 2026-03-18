using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiRoleReportPermissionRepositoryTests : IDisposable
{
    private readonly MiBffReadDbContext _context;
    private readonly IMiRoleReportPermissionRepository _repository;

    public MiRoleReportPermissionRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = TestDbContextHelper.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiRoleReportPermissionRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiRoleReportPermissionRepository(null!)));
    }

    [Fact]
    public async Task GetByRoleId_ReturnsItems()
    {
        var items = await _repository.GetByRoleIdAsync(TestDataSeeder.RoleAdmin, TestContext.Current.CancellationToken);
        Assert.NotNull(items);
        Assert.True(items.Any());
        Assert.All(items, i => Assert.Equal(TestDataSeeder.RoleAdmin, i.RoleId));
    }

    [Fact]
    public async Task GetByRoleId_NotFound_ReturnsEmpty()
    {
        var result = await _repository.GetByRoleIdAsync(Guid.Empty, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task List_ReturnsItems()
    {
        var items = await _repository.ListAsync(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(items);
        Assert.Equal(3, items.Count());
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.RoleAdmin, TestDataSeeder.ReportA, TestDataSeeder.PermissionView);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_Returns_Null_Returns_Null()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.RoleAdmin, TestDataSeeder.ReportB, TestDataSeeder.PermissionView);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckExists_ForKnown_ReturnsTrue()
    {
        var exists = await _repository.ExistsAsync(i => i.RoleId == TestDataSeeder.RoleAdmin && i.ReportId == TestDataSeeder.ReportA, CancellationToken.None);
        Assert.True(exists);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.RoleId == TestDataSeeder.RoleAdmin && i.ReportId == TestDataSeeder.ReportB && i.PermissionId == TestDataSeeder.PermissionView, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.RoleId == TestDataSeeder.RoleAdmin && i.ReportId == TestDataSeeder.ReportA, orderBy: i => i.OrderByDescending(o => o.Granted), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(2, result!.Count());
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.RoleId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.Granted), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}