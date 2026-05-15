using System.Net;
using AutoFixture;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Testing.Support.Contexts;
using Cads.Cds.MiBff.Testing.Support.SpecimenBuilders;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetGbCattleDeathsTests
{
    private readonly Fixture _fixture;

    private static string Endpoint =>
        $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleDeathsReportKey}";

    public GetGbCattleDeathsTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new IgnoreNavigationProperties());
    }

    [Fact]
    public async Task GivenInvalidYear_WhenGetGbCattleDeathsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(0);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_AndNoRecordsFound_WhenGetGbCattleDeathsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(2026, []);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndSingleRecordFoundNullable_WhenGetGbCattleDeathsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(2026, [
            _fixture.Build<MiDeathSummary>()
                .With(x => x.Sex, () => null)
                .Create()
        ]);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndRecordsFound_WhenGetGbCattleDeathsRequested_ShouldSucceed()
    {
        var deathSummaryData = _fixture.CreateMany<MiDeathSummary>(5).ToList();

        var response = await ExecuteTest(2026, deathSummaryData);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    private static async Task<HttpResponseMessage?> ExecuteTest(int year, List<MiDeathSummary>? deathSummaries = null)
    {
        var request = new GetGbCattleDeathsRequest { Year = year };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        List<Action<TestMiBffReadDbContext>> dataOverrides = deathSummaries == null ? [] : [(db) => { db.DeathSummaries = deathSummaries; }];

        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true, dataOverrides: dataOverrides);
        using var client = factory.CreateClient();
        client.AddJwt();

        return await client.PostAsync(Endpoint, payload, TestContext.Current.CancellationToken);
    }
}