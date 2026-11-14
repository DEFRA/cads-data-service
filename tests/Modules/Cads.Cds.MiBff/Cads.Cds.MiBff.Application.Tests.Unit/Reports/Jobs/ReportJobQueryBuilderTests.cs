using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Core.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Jobs;

public class ReportJobQueryBuilderTests
{
    private readonly ReportJobQueryBuilder _sut;

    public ReportJobQueryBuilderTests()
    {
        var services = new ServiceCollection();
        services.AddScoped<GetGbCattleDeathsHandler>();

        _sut = new ReportJobQueryBuilder(
            new ReportRegistry([typeof(IReportHandler).Assembly]),
            services.BuildServiceProvider());
    }

    private static MiReportJob NewJob(
        string reportKey = GetGbCattleDeathsQuery.ExpectedKey,
        string requestPayload = "{\"reportKey\":\"gb_cattle_deaths\",\"year\":2024}") => new()
        {
            ReportJobId = Guid.NewGuid(),
            ReportKey = reportKey,
            ExternalSubject = "user@example.test",
            RequestPayload = requestPayload,
            Status = ReportJobStatus.Pending
        };

    [Fact]
    public void Build_WithGbCattleDeathsJob_ReturnsQueryFromStoredPayload()
    {
        var query = _sut.Build(NewJob());

        var deathsQuery = query.Should().BeOfType<GetGbCattleDeathsQuery>().Subject;
        deathsQuery.Year.Should().Be(2024);
        deathsQuery.ReportKey.Should().Be(GetGbCattleDeathsQuery.ExpectedKey);
    }

    [Fact]
    public void Build_UsesReportKeyFromJob_NotFromStoredPayload()
    {
        var query = _sut.Build(NewJob(requestPayload: "{\"reportKey\":\"something_else\",\"year\":2020}"));

        ((GetGbCattleDeathsQuery)query).ReportKey.Should().Be(GetGbCattleDeathsQuery.ExpectedKey);
    }

    [Fact]
    public void Build_WithYearAsString_ReadsYear()
    {
        var query = _sut.Build(NewJob(requestPayload: "{\"year\":\"2023\"}"));

        ((GetGbCattleDeathsQuery)query).Year.Should().Be(2023);
    }

    [Fact]
    public void Build_WithUnsupportedReportKey_ThrowsNonRetryable()
    {
        var act = () => _sut.Build(NewJob(reportKey: "gb_cattle_imports"));

        act.Should().Throw<NonRetryableException>().WithMessage("*gb_cattle_imports*not supported*");
    }

    [Fact]
    public void Build_WithInvalidPayload_ThrowsNonRetryable()
    {
        var act = () => _sut.Build(NewJob(requestPayload: "not json"));

        act.Should().Throw<NonRetryableException>().WithMessage("*could not be deserialised*");
    }

    [Fact]
    public void Build_WithNullPayload_ThrowsNonRetryable()
    {
        var act = () => _sut.Build(NewJob(requestPayload: "null"));

        act.Should().Throw<NonRetryableException>().WithMessage("*was empty*");
    }
}