using Cads.Cds.BuildingBlocks.Testing.Support.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Protected;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiImportSummaryRepositoryTests
{
    [Fact]
    public async Task GetImportSummaryAsync_ShouldReturnList()
    {
        var from = new DateOnly(2026, 3, 1);
        var to = new DateOnly(2026, 5, 1);

        var expected = new List<MiImportSummary>
        {
            new() { MonthYear = new DateTime(2026, 3, 1)},
            new() { MonthYear = new DateTime(2026, 4, 1)}
        }.AsQueryable();

        var asyncQueryable = new TestAsyncEnumerable<MiImportSummary>(expected);

        var miBirthSummaryRepositoryMock = new Mock<MiImportSummaryRepository>(MockBehavior.Strict, [CreateFakeDbContext()])
        {
            CallBase = true
        };

        miBirthSummaryRepositoryMock
            .Protected()
            .Setup<IQueryable<MiImportSummary>>("QueryImportSummary", from, to)
            .Returns(asyncQueryable);

        var result = await miBirthSummaryRepositoryMock.Object.GetImportSummaryAsync(from, to, TestContext.Current.CancellationToken);

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