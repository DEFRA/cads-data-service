using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiEffectiveReportPermissionRepositoryTests : IDisposable
{
    private readonly MiBffReadDbContext _context;
    private readonly IMiEffectiveReportPermissionRepository _repository;

    public MiEffectiveReportPermissionRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = TestDbContextHelper.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);

        _repository = new MiEffectiveReportPermissionRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiEffectiveReportPermissionRepository(null!)));
    }

    [Fact]
    public async Task GetByUserEmail_Found_ReturnsItems()
    {
        var result = await _repository.GetByUserEmailAsync(TestDataSeeder.User1Email, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(TestDataSeeder.User1Email, result!.First().Email);
        Assert.Equal(TestDataSeeder.ReportAKey, result!.First().ReportKey);
        Assert.Equal(TestDataSeeder.ReportATitle, result!.First().Title);
        Assert.Equal(TestDataSeeder.ReportADescription, result!.First().Description);
    }

    [Fact]
    public async Task GetByUserEmail_NotFound_ReturnsEmpty()
    {
        var result = await _repository.GetByUserEmailAsync(string.Empty, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task List_ReturnsItems()
    {
        var result = await _repository.ListAsync(i=> i.Where(i => i.Granted), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.User1Email, TestDataSeeder.ReportAKey);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByKeyAsync(string.Empty, TestDataSeeder.ReportAKey);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckExists_ForKnown_ReturnsTrue()
    {
        var result = await _repository.ExistsAsync(i => i.Email == TestDataSeeder.User1Email, TestContext.Current.CancellationToken);
        Assert.True(result);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.Email == string.Empty, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.Email == TestDataSeeder.User1Email && i.ReportKey == TestDataSeeder.ReportAKey, orderBy: i => i.OrderByDescending(o => o.ReportKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.User1Email, result!.First().Email);
        Assert.Equal(TestDataSeeder.ReportAKey, result!.First().ReportKey);
        Assert.Equal(TestDataSeeder.ReportATitle, result!.First().Title);
        Assert.Equal(TestDataSeeder.ReportADescription, result!.First().Description);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.Email == string.Empty, orderBy: i => i.OrderByDescending(o => o.ReportKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}