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
        var endpoint = "/api/v1/bff/mi/dashboard";

        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();
        var result = await response!.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        ValidateResponseWithMetaData(result, endpoint);

        var data = MiBffTestFixture.GetResponseData<DashboardListingDto>(result.Data);
        data.Should().NotBeNull();
        data.Results.Should().NotBeNull().And.HaveCount(10);
    }

    private static void ValidateResponseWithMetaData(JsonResponseWithMetaData response, string expectedEndpoint)
    {
        response.Meta.Should().NotBeNull();
        response.Meta.RequestId.Should().NotBeNull();
        response.Meta.Status.Should().NotBeNull().And.Contain("Request successful");
        response.Meta.Timestamp.Should().NotBeNull();

        response.Links.Should().NotBeNull();
        response.Links.Self.Should().NotBeNull().And.Contain(expectedEndpoint);

        response.Data.Should().NotBeNull();
    }
}