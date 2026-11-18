using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Core.Domain.Entities;
using FluentAssertions;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Routing;

public class ReportKeyModelRegistryTests
{
    private readonly ReportKeyModelRegistry _sut = new();

    [Theory]
    [InlineData("gb_cattle_deaths", typeof(MiDeathSummary))]
    [InlineData("gb_cattle_registrations", typeof(MiBirthSummary))]
    [InlineData("gb_cattle_imports", typeof(MiImportSummary))]
    public void TryGetModelType_WithKnownReportKey_ReturnsTrueAndExpectedModelType(string reportKey, Type expected)
    {
        var found = _sut.TryGetModelType(reportKey, out var modelType);

        found.Should().BeTrue();
        modelType.Should().Be(expected);
    }

    [Fact]
    public void TryGetModelType_WithKnownReportKeys_MatchQueryExpectedKeys()
    {
        _sut.TryGetModelType(GetGbCattleDeathsQuery.ExpectedKey, out var deathsType).Should().BeTrue();
        deathsType.Should().Be(typeof(MiDeathSummary));

        _sut.TryGetModelType(GetGbCattleRegistrationsQuery.ExpectedKey, out var birthsType).Should().BeTrue();
        birthsType.Should().Be(typeof(MiBirthSummary));

        _sut.TryGetModelType(GetGbCattleImportsQuery.ExpectedKey, out var importsType).Should().BeTrue();
        importsType.Should().Be(typeof(MiImportSummary));
    }

    [Fact]
    public void TryGetModelType_WithUnknownReportKey_ReturnsFalse()
    {
        var found = _sut.TryGetModelType("unknown_report", out var modelType);

        found.Should().BeFalse();
        modelType.Should().BeNull();
    }
}