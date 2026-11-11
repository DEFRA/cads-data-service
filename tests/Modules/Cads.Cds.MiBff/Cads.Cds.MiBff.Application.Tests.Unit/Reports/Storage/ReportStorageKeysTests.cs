using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Storage;
using FluentAssertions;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Storage;

public class ReportStorageKeysTests
{
    [Fact]
    public void For_GbCattleDeathsQuery_BuildsExpectedKey()
    {
        var jobId = Guid.Parse("3f2b8c1e-0d4a-4b6e-9a1f-2c3d4e5f6a7b");

        ReportStorageKeys.For(typeof(GetGbCattleDeathsQuery), jobId)
            .Should().Be("mibff-reports/gbcattledeaths/3f2b8c1e-0d4a-4b6e-9a1f-2c3d4e5f6a7b");
    }

    [Theory]
    [InlineData(typeof(GetGbCattleDeathsQuery), "gbcattledeaths")]
    [InlineData(typeof(GetGbCattleImportsQuery), "gbcattleimports")]
    [InlineData(typeof(ReportWithoutAffixes), "reportwithoutaffixes")]
    [InlineData(typeof(Query), "query")]
    public void ReportSegment_StripsGetPrefixAndQuerySuffix(Type queryType, string expected)
    {
        ReportStorageKeys.ReportSegment(queryType).Should().Be(expected);
    }

    private sealed class ReportWithoutAffixes;

    private sealed class Query;
}