using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;
using FluentAssertions;
using Xunit;

namespace Cads.Cds.Tests.Integration;

[Collection("CadsIntegration"), Trait("Dependence", "testcontainers")]
public class HealthcheckEndpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidHealthCheckRequest_ShouldSucceed()
    {
        var response = await apiContainerFixture.HttpClient.GetAsync("health", TestContext.Current.CancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();
        responseBody.Should().NotBeNullOrEmpty().And.Contain("\"status\": \"Healthy\"");
        responseBody.Should().NotContain("\"status\": \"Degraded\"");
        responseBody.Should().NotContain("\"status\": \"Unhealthy\"");
    }
}