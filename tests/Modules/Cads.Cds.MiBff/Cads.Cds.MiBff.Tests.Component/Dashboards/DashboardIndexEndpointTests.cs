using System.Net.Http.Json;
using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Component.Dashboards;

public class DashboardIndexEndpointTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GetDashboards_Endpoint_ReturnsOk()
    {
        var endpoint = "/api/v1/bff/report/dashboard";

        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();
        var data = await response!.Content.ReadFromJsonAsync<List<ReportListingDto>>(TestContext.Current.CancellationToken);
        data.Should().NotBeNull().And.HaveCount(10);
    }
}