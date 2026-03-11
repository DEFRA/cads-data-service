using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using FluentAssertions;

namespace Cads.Cds.Tests.Integration.Endpoints;

[Collection("CadsIntegration"), Trait("Dependence", "testcontainers")]
public class HealthcheckEndpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidHttpHealthCheckRequest_ShouldSucceed()
    {
        var response = await apiContainerFixture.HttpClient.GetAsync("health", TestContext.Current.CancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
    
        response.EnsureSuccessStatusCode();
        responseBody.Should().NotBeNullOrEmpty().And.Contain("\"status\": \"Healthy\"");
        responseBody.Should().NotContain("\"status\": \"Degraded\"");
        responseBody.Should().NotContain("\"status\": \"Unhealthy\"");
    }
    
    [Fact]
    public async Task GivenValidHttpsHealthCheckRequest_ShouldSucceed()
    {
        var response = await apiContainerFixture.HttpsClient.GetAsync("health", TestContext.Current.CancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
    
        response.EnsureSuccessStatusCode();
        responseBody.Should().NotBeNullOrEmpty().And.Contain("\"status\": \"Healthy\"");
        responseBody.Should().NotContain("\"status\": \"Degraded\"");
        responseBody.Should().NotContain("\"status\": \"Unhealthy\"");
    }
}