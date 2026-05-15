using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using FluentAssertions;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class StubMiImportSummaryRepositoryTests
{
    [Fact]
    public async Task CanGetStubData()
    {
        var sut = new StubMiImportSummaryRepository();
        var results = await sut.GetImportSummaryAsync(new DateOnly(2024, 1, 1), new DateOnly(2025, 1, 1), TestContext.Current.CancellationToken);
        results.Should().NotBeEmpty();
    }
}