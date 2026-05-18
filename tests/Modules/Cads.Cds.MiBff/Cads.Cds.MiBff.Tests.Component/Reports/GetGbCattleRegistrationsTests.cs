using AutoFixture;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Testing.Support.SpecimenBuilders;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetGbCattleRegistrationsTests
{
    private readonly Fixture _fixture;

    private static string Endpoint =>
        $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleRegistrationsReportKey}";

    public GetGbCattleRegistrationsTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new IgnoreNavigationProperties());
    }

    [Fact]
    public async Task GivenInvalidYear_WhenGetGbCattleRegistrationsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(0, 4);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenInvalidMonth_WhenGetGbCattleRegistrationsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(2026, 0);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_AndNoRecordsFound_WhenGetGbCattleRegistrationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(2026, 1);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndSingleRecordFoundNullable_WhenGetGbCattleRegistrationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(2026, 4);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndRecordsFound_WhenGetGbCattleRegistrationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(2026, 4);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    private static async Task<HttpResponseMessage?> ExecuteTest(int year, int month)
    {
        var request = new GetGbCattleRegistrationsRequest { Year = year, Month = month };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        var client = factory.CreateClient();
        client.AddJwt();

        return await client.PostAsync(Endpoint, payload, TestContext.Current.CancellationToken);
    }
}