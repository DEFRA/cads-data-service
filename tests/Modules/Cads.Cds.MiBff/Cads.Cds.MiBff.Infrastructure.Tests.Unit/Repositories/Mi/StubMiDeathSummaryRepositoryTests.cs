using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using FluentAssertions;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class StubMiDeathSummaryRepositoryTests
{
    [Fact]
    public async Task StubMiDeathSummaryRepository_ShouldReturnStub()
    {
        var sut = new StubMiDeathSummaryRepository();
        var result = await sut.GetDeathSummaryAsync(new DateOnly(2024, 1, 1), new DateOnly(2025, 1, 1), TestContext.Current.CancellationToken);

        result.Should().NotBeEmpty();
    }
}