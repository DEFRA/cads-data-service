using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiUserRepositoryTests : IDisposable
{
    private readonly MiBffReadDbContext _context;
    private readonly IMiUserRepository _repository;

    public MiUserRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextFactory.CreateInMemoryDbContext<MiBffReadDbContext>(Guid.NewGuid().ToString());
        TestDataSeeder.Seed(_context);
        _repository = new MiUserRepository(_context);
    }

    [Fact]
    public async Task Instantiate_WithNullContext_ThrowsException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new MiUserRepository(null!)));
    }

    [Fact]
    public async Task GetByUserId_Found_ReturnsItem()
    {
        var result = await _repository.GetByUserIdAsync(TestDataSeeder.User1, TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.User1, result!.UserId);
    }

    [Fact]
    public async Task GetByUserId_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByUserIdAsync(Guid.Empty, TestContext.Current.CancellationToken);
        Assert.Null(result);
    }

    [Fact]
    public async Task List_ReturnsItems()
    {
        var result = await _repository.ListAsync(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.True(result.Any());
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetByKey_Found_ReturnsItem()
    {
        var result = await _repository.GetByKeyAsync(TestDataSeeder.User1);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByKey_NotFound_Returns_Null()
    {
        var result = await _repository.GetByKeyAsync(Guid.Empty);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckExists_ForKnown_ReturnsTrue()
    {
        var result = await _repository.ExistsAsync(i => i.UserId == TestDataSeeder.User1, TestContext.Current.CancellationToken);
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
        var result = await _repository.FindAsync(i => i.UserId == TestDataSeeder.User1, orderBy: i => i.OrderByDescending(o => o.CreatedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(TestDataSeeder.User1, result[0]!.UserId);
    }

    [Fact]
    public async Task Find_ForUnKnown_ReturnsEmpty()
    {
        var result = await _repository.FindAsync(i => i.UserId == Guid.Empty, orderBy: i => i.OrderByDescending(o => o.CreatedAt), cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}