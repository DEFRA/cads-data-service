using Cads.Cds.BuildingBlocks.Testing.Support.Payloads;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Testing.Support.Constants;
using Cads.Cds.Ingester.Tests.Component.TestFixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;

using V1 = Cads.Cds.Ingester.Controllers.AnimalMovements.v1;
using V2 = Cads.Cds.Ingester.Controllers.AnimalMovements.v2;

namespace Cads.Cds.Ingester.Tests.Component.AnimalMovementsEndpoints;

public class AnimalMovementsEndpointTests(IngesterTestFixture testFixture) : IClassFixture<IngesterTestFixture>
{
    [Fact]
    public async Task AnimalMovements_EnpointV1_Passes_And_Returns_Accepted()
    {
        // Arrange
        var nation = Nation.Wales;
        var requestBody = TestDataPayloadFactory.CreatePayload<V1.AnimalMovementMessage>();

        var payload = JsonConvert.SerializeObject(requestBody);
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV1, nation);

        // Act
        var result = await testFixture.HttpClient.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"), TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccessStatusCode.Should().BeTrue();
        var responseBody = await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);
        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLower());
        responseData.RecordCount.Should().Be(1);
    }

    [Fact]
    public async Task AnimalMovements_EnpointV2_Passes_And_Returns_Accepted()
    {
        // Arrange
        var nation = Nation.Wales;
        var requestBody = TestDataPayloadFactory.CreatePayload<V2.EventMessage>();

        var payload = JsonConvert.SerializeObject(requestBody);
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);

        // Act
        var result = await testFixture.HttpClient.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"), TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccessStatusCode.Should().BeTrue();
        var responseBody = await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);
        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLower());
        responseData.RecordCount.Should().Be(1);
    }


    [Fact]
    public async Task AnimalMovements_EndpointV2_Returns_BadRequest_For_InvalidJson()
    {
        // Arrange
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);
        using var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var result = await testFixture.HttpClient.PostAsync(
            endpoint,
            content,
            TestContext.Current.CancellationToken);

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AnimalMovements_EndpointV2_Returns_BadRequest_For_EmptyRequestBody()
    {
        // Arrange
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);
        using var content = new StringContent(
            string.Empty,
            Encoding.UTF8,
            "application/json");

        // Act
        var result = await testFixture.HttpClient.PostAsync(
            endpoint,
            content,
            TestContext.Current.CancellationToken);

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AnimalMovements_EndpointV2_Returns_BadRequest_When_Event_Is_Missing()
    {
        // Arrange
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);
        using var content = new StringContent(
            "{}",
            Encoding.UTF8,
            "application/json");

        // Act
        var result = await testFixture.HttpClient.PostAsync(
            endpoint,
            content,
            TestContext.Current.CancellationToken);

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}