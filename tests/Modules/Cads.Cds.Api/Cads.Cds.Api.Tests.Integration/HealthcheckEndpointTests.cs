using FluentAssertions;

namespace Cads.Cds.Api.Tests.Integration;

[Collection("Integration"), Trait("Dependence", "testcontainers")]
public class HealthcheckEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private readonly ApiContainerFixture _apiContainerFixture = apiContainerFixture;

    [Fact]
    public async Task GivenValidHealthCheckRequest_ShouldSucceed()
    {
        var response = await _apiContainerFixture.HttpClient.GetAsync("health", TestContext.Current.CancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();
        responseBody.Should().NotBeNullOrEmpty().And.Contain("\"status\": \"Healthy\"");
        responseBody.Should().NotContain("\"status\": \"Degraded\"");
        responseBody.Should().NotContain("\"status\": \"Unhealthy\"");
    }
}