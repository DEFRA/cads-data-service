using Cads.Cds.ApiSurface.Dtos.Common.JsonResponsesWrap;
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
        using var client = _testFixture.Factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/bff/ukv/holdings", TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        result.Meta.Should().NotBeNull();
        result.Meta.RequestId.Should().NotBeNull();
        result.Meta.Status.Should().NotBeNull().And.Contain("Request successful");
        result.Meta.Timestamp.Should().NotBeNull();

        result.Links.Should().NotBeNull();
        result.Links.Self.Should().NotBeNull().And.Contain("api/v1/bff/ukv/holdings");

        var dataStr = JsonSerializer.Serialize(result.Data);
        var data = JsonSerializer.Deserialize<JsonResponseData<HoldingDTO>>(dataStr);
        data.Should().NotBeNull();
        data.Results.Should().NotBeNull().And.HaveCount(5);
    }

    [Fact]
    public async Task GetHoldingsByCph_Endpoint_PassesCph_And_ReturnsOk()
    {
        // Arrange
        using var client = _testFixture.Factory.CreateClient();

        var cph = "ABC123";

        // Act
        var response = await client.GetAsync($"/api/v1/bff/ukv/holdings/{cph}", TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var result = await response.Content.ReadFromJsonAsync<JsonResponseWithMetaData>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();

        result.Meta.Should().NotBeNull();
        result.Meta.RequestId.Should().NotBeNull();
        result.Meta.Status.Should().NotBeNull().And.Contain("Request successful");
        result.Meta.Timestamp.Should().NotBeNull();

        result.Links.Should().NotBeNull();
        result.Links.Self.Should().NotBeNull().And.Contain("api/v1/bff/ukv/holdings");

        var dataStr = JsonSerializer.Serialize(result.Data);
        var data = JsonSerializer.Deserialize<JsonResponseData<HoldingDTO>>(dataStr);
        data.Should().NotBeNull();
        data.Results.Should().NotBeNull().And.HaveCount(1);
    }
}