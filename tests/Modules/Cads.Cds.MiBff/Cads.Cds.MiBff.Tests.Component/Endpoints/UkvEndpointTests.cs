using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Component.Endpoints;

public class UkvEndpointTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
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
    }

    [Fact]
    public async Task GetHoldingsByCph_Endpoint_PassesCph_And_ReturnsNotFound()
    {
        // Arrange
        using var client = _testFixture.Factory.CreateClient();

        var cph = "ABC456";

        // Act
        var response = await client.GetAsync($"/api/v1/bff/ukv/holdings/{cph}", TestContext.Current.CancellationToken);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
    }
}