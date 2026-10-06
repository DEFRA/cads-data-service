using Cads.Cds.StorageBridge.Endpoints;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Tests.Unit.Endpoints;

public class StorageKeyMatcherTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("contains")]
    public void Create_ContainsModes_ShouldMatchSubstringsCaseInsensitively(string? mode)
    {
        var matches = StorageKeyMatcher.Create("report", mode);

        matches.Should().NotBeNull();
        matches!("data/report-2026.csv").Should().BeTrue();
        matches("data/REPORT-2026.csv").Should().BeTrue();
        matches("data/summary.csv").Should().BeFalse();
    }

    [Fact]
    public void Create_ContainsMode_ShouldTreatRegexMetacharactersLiterally()
    {
        var matches = StorageKeyMatcher.Create("a.c", "contains");

        matches.Should().NotBeNull();
        matches!("data/xa.cy.csv").Should().BeTrue();
        matches("data/abc.csv").Should().BeFalse();
    }

    [Fact]
    public void Create_ContainsMode_WithEmptyPattern_ShouldMatchEveryKey()
    {
        var matches = StorageKeyMatcher.Create(string.Empty, "contains");

        matches.Should().NotBeNull();
        matches!("data/report.csv").Should().BeTrue();
        matches(string.Empty).Should().BeTrue();
    }

    [Theory]
    [InlineData("*", "data/report.csv")]
    [InlineData("*.csv", "report.csv")]
    [InlineData("*.csv", "data/archive/report.csv")]
    [InlineData("data/*", "data/report.csv")]
    [InlineData("data/*.csv", "data/report.csv")]
    [InlineData("report-?.csv", "report-1.csv")]
    [InlineData("report.csv", "REPORT.CSV")]
    [InlineData("*REPORT*", "data/report-2026.csv")]
    [InlineData("report*", "report.csv")]
    public void Create_GlobMode_ShouldMatchWhenTheWholeKeyFitsThePattern(string pattern, string key)
    {
        var matches = StorageKeyMatcher.Create(pattern, "glob");

        matches.Should().NotBeNull();
        matches!(key).Should().BeTrue();
    }

    [Theory]
    [InlineData("*.csv", "report.csv.gz")]
    [InlineData("data/*.csv", "other/report.csv")]
    [InlineData("report-?.csv", "report-10.csv")]
    [InlineData("report-?.csv", "report-.csv")]
    [InlineData("report.csv", "reportXcsv")]
    [InlineData("report", "data/report.csv")]
    public void Create_GlobMode_ShouldNotMatchPartialOrDivergentKeys(string pattern, string key)
    {
        var matches = StorageKeyMatcher.Create(pattern, "glob");

        matches.Should().NotBeNull();
        matches!(key).Should().BeFalse();
    }

    [Fact]
    public void Create_GlobMode_ShouldTreatRegexMetacharactersLiterally()
    {
        var matches = StorageKeyMatcher.Create("report(1)+[a].csv", "glob");

        matches.Should().NotBeNull();
        matches!("report(1)+[a].csv").Should().BeTrue();
        matches("report1a.csv").Should().BeFalse();
    }

    [Fact]
    public void Create_GlobMode_WithEmptyPattern_ShouldMatchOnlyAnEmptyKey()
    {
        var matches = StorageKeyMatcher.Create(string.Empty, "glob");

        matches.Should().NotBeNull();
        matches!(string.Empty).Should().BeTrue();
        matches("report.csv").Should().BeFalse();
    }

    [Theory]
    [InlineData("^data/", "data/report.csv")]
    [InlineData(@"\.csv$", "data/report.csv")]
    [InlineData("report", "data/report.csv")]
    [InlineData("REPORT", "data/report.csv")]
    [InlineData("report-[0-9]+", "data/report-2026.csv")]
    public void Create_RegexMode_ShouldMatchAnywhereInTheKeyCaseInsensitively(string pattern, string key)
    {
        var matches = StorageKeyMatcher.Create(pattern, "regex");

        matches.Should().NotBeNull();
        matches!(key).Should().BeTrue();
    }

    [Theory]
    [InlineData("^data/", "archive/report.csv")]
    [InlineData(@"\.csv$", "data/report.json")]
    [InlineData("report-[0-9]+", "data/report-final.csv")]
    public void Create_RegexMode_ShouldNotMatchKeysOutsideThePattern(string pattern, string key)
    {
        var matches = StorageKeyMatcher.Create(pattern, "regex");

        matches.Should().NotBeNull();
        matches!(key).Should().BeFalse();
    }

    [Theory]
    [InlineData("[")]
    [InlineData("(")]
    [InlineData("*")]
    [InlineData(@"(?<")]
    public void Create_RegexMode_WithAnUnparseablePattern_ShouldReturnNull(string pattern)
    {
        StorageKeyMatcher.Create(pattern, "regex").Should().BeNull();
    }

    [Fact]
    public void Create_RegexMode_WhenMatchingTimesOut_ShouldReportNoMatchInsteadOfThrowing()
    {
        // Catastrophic backtracking: the matcher is expected to swallow the timeout.
        var matches = StorageKeyMatcher.Create("^(a+)+$", "regex");

        matches.Should().NotBeNull();
        matches!(new string('a', 50) + "b").Should().BeFalse();
    }

    [Theory]
    [InlineData("wildcard")]
    [InlineData("GLOB")]
    [InlineData("Contains")]
    [InlineData(" contains ")]
    [InlineData("like")]
    public void Create_WithAnUnknownMode_ShouldReturnNull(string mode)
    {
        StorageKeyMatcher.Create("report", mode).Should().BeNull();
    }
}