using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Cads.Cds.MiBff.Tests.Component.UkvEndpoints.Holdings;

public class UkvHoldingsEndpointTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GetHoldings_Endpoint_UsesDefaults_And_ReturnsOk()
    {
        // Arrange
        var endpoint = TestEndpointConstants.BffUkvHoldingsEndpoint;

        // Act
        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        ValidateResponseWithMetaData(result, endpoint);

        var data = GetResponseData(result.Data);
        data.Should().NotBeNull();
        data.Results.Should().NotBeNull().And.HaveCount(5);
    }

    [Fact]
    public async Task GetHoldingsByCph_Endpoint_PassesCph_And_ReturnsOk()
    {
        // Arrange
        var cph = "ABC123";
        var endpoint = string.Format(TestEndpointConstants.BffUkvHoldingsByCphEndpoint, cph);

        // Act
        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        ValidateResponseWithMetaData(result, endpoint);

        var data = GetResponseData(result.Data);
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

    private static JsonResponseData<HoldingDTO>? GetResponseData(object? responseData)
    {
        if (responseData == null) return null;

        return JsonSerializer.Deserialize<JsonResponseData<HoldingDTO>>(JsonSerializer.Serialize(responseData));
    }
}