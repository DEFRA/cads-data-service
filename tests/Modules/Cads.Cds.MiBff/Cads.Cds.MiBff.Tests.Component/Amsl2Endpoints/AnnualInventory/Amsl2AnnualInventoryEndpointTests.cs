using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.MiBff.Core.DTOs.Amls2;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net.Http.Json;

namespace Cads.Cds.MiBff.Tests.Component.Amsl2Endpoints.AnnualInventory;

public class Amsl2AnnualInventoryEndpointTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GetAnualInventory_Endpoint_Uses_Defaults_And_Returns_Ok()
    {
        // Arrange
        var endpoint = TestEndpointConstants.BffAmsl2AnnualInventoryEndpoint;

        // Act
        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        ValidateResponseWithMetaData(result, endpoint);

        var data = MiBffTestFixture.GetResponseData<Amsl2Dto>(result.Data);
        data.Should().NotBeNull();
        data.Results.Should().NotBeNull().And.HaveCount(5);
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