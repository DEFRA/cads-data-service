using Cads.Cds.BuildingBlocks.Testing.Support.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Protected;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiDeathSummaryRepositoryTests
{
    [Fact]
    public async Task GetDeathSummaryAsync_ShouldReturnList()
    {
        var from = new DateOnly(2026, 3, 1);
        var to = new DateOnly(2026, 5, 1);

        var expected = new List<MiDeathSummary>
        {
            new() { DeathYear = 2026, DeathMonth = "March" },
            new() { DeathYear = 2026, DeathMonth = "April" }
        }.AsQueryable();

        var asyncQueryable = new TestAsyncEnumerable<MiDeathSummary>(expected);

        var miBirthSummaryRepositoryMock = new Mock<MiDeathSummaryRepository>(MockBehavior.Strict, [CreateFakeDbContext()])
        {
            CallBase = true
        };

        miBirthSummaryRepositoryMock
            .Protected()
            .Setup<IQueryable<MiDeathSummary>>("QueryDeathSummary", from, to)
            .Returns(asyncQueryable);

        var result = await miBirthSummaryRepositoryMock.Object.GetDeathSummaryAsync(from, to, TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(expected);
    }

    private static MiBffReadDbContext CreateFakeDbContext()
    {
        var options = new DbContextOptionsBuilder<MiBffReadDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new MiBffReadDbContext(options);
    }
}