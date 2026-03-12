using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net.Http.Json;

namespace Cads.Cds.MiBff.Tests.Component.Amsl2Endpoints.DetailedMovements;

public class DetailedMovementsyEndpointTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GetDetailedMovementById_Endpoint_Passes_AnimalId_And_Returns_Ok()
    {
        // Arrange
        var movementId = "e6f2cb0f-6702-4060-951f-b32e018b3b92";
        var endpoint = string.Format(TestEndpointConstants.BffAmsl2DetailedMovementByIdEndpoint, movementId);

        // Act
        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        ValidateResponseWithMetaData(result, endpoint);

        var data = MiBffTestFixture.GetResponseData<Amsl2Dto>(result.Data);
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