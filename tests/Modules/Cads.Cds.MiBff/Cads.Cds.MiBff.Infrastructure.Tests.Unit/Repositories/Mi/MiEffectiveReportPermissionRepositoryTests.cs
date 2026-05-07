using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiEffectiveReportPermissionRepositoryTests : IDisposable
{
    private readonly TestMiBffReadDbContext _context;
    private readonly MiEffectiveReportPermissionRepository _repository;

    public MiEffectiveReportPermissionRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        var options = new DbContextOptionsBuilder<MiBffReadDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new TestMiBffReadDbContext(options);
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
        var result = await _repository.GetActiveByExternalSubjectAsync(TestDataSeeder.User1Email, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(TestDataSeeder.User1Email, result[0].ExternalSubject);
        Assert.Equal(TestDataSeeder.ReportAKey, result[0].ReportKey);
        Assert.Equal(TestDataSeeder.ReportATitle, result[0].Title);
        Assert.Equal(TestDataSeeder.ReportADescription, result[0].Description);
    }

    [Fact]
    public async Task GetByUserEmail_NotFound_ReturnsEmpty()
    {
        var result = await _repository.GetActiveByExternalSubjectAsync(string.Empty, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task List_ReturnsItems()
    {
        var result = await _repository.ListAsync(i => i.Where(i => i.Granted), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
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
        var result = await _repository.ExistsAsync(i => i.ExternalSubject == TestDataSeeder.User1Email, TestContext.Current.CancellationToken);
        Assert.True(result);
    }

    [Fact]
    public async Task CheckExists_ForUnknown_ReturnsFalse()
    {
        var result = await _repository.ExistsAsync(i => i.ExternalSubject == string.Empty, TestContext.Current.CancellationToken);
        Assert.False(result);
    }

    [Fact]
    public async Task Find_ForKnown_ReturnsItem()
    {
        var result = await _repository.FindAsync(i => i.ExternalSubject == TestDataSeeder.User1Email && i.ReportKey == TestDataSeeder.ReportAKey, orderBy: i => i.OrderByDescending(o => o.ReportKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.User1Email, result[0].ExternalSubject);
        Assert.Equal(TestDataSeeder.ReportAKey, result[0].ReportKey);
        Assert.Equal(TestDataSeeder.ReportATitle, result[0].Title);
        Assert.Equal(TestDataSeeder.ReportADescription, result[0].Description);
    }

    [Fact]
    public async Task ProjectAsync_WithDtoProjection_ReturnsDtoInstances()
    {
        // arrange
        // simple DTO used for projection verification
        var projected = await _repository.ProjectAsync(
            q => q.Where(x => x.Granted)
                  .Select(x => new ProjectionDto { ExternalSubject = x.ExternalSubject, ReportKey = x.ReportKey }),
            asNoTracking: true,
            cancellationToken: CancellationToken.None);

        // assert
        Assert.NotNull(projected);
        Assert.Equal(2, projected.Count);
        Assert.All(projected, p => Assert.False(string.IsNullOrWhiteSpace(p.ExternalSubject)));
        var keys = projected.Select(p => p.ReportKey).OrderBy(k => k).ToList();
        Assert.Contains(TestDataSeeder.ReportAKey, keys);
        Assert.Contains(TestDataSeeder.ReportBKey, keys);
    }

    private class ProjectionDto
    {
        public required string ExternalSubject { get; set; }
        public required string ReportKey { get; set; }
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.ExternalSubject == string.Empty, orderBy: i => i.OrderByDescending(o => o.ReportKey), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}