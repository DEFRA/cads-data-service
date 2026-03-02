using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net.Http.Json;

namespace Cads.Cds.MiBff.Tests.Component.UkvEndpoints.Zones;

public class UkvZonesEndpointTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GetZones_Endpoint_Uses_Defaults_And_Returns_Ok()
    {
        // Arrange
        var endpoint = TestEndpointConstants.BffUkvZonesEndpoint;

        // Act
        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        ValidateResponseWithMetaData(result, endpoint);

        var data = MiBffTestFixture.GetResponseData<UkvDto>(result.Data);
        data.Should().NotBeNull();
        data.Results.Should().NotBeNull().And.HaveCount(5);
    }

    [Fact]
    public async Task GetZonesByZoneId_Endpoint_Passes_ZoneId_And_Returns_Ok()
    {
        // Arrange
        var zoneId = "e6f2cb0f-6702-4060-951f-b32e018b3b92";
        var endpoint = string.Format(TestEndpointConstants.BffUkvZonesByZoneIdEndpoint, zoneId);

        // Act
        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        ValidateResponseWithMetaData(result, endpoint);

        var data = MiBffTestFixture.GetResponseData<UkvDto>(result.Data);
        data.Should().NotBeNull();
        data.Results.Should().NotBeNull().And.HaveCount(1);
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