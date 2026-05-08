using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.MiBff.Testing.Support.Constants;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Integration.UkvEndpoints.Holdings;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class UkvHoldingsEndpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidCph_WhenHoldingByCphRequested_ShouldSucceed()
    {
        var cph = "ABC123";
        var endpoint = string.Format(TestEndpointConstants.BffUkvHoldingsByCphEndpoint, cph);
        var client = await apiContainerFixture.CreateAzureAdClientAsync();

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        responseBody.Should().NotBeNull();
    }
}