using Cads.Cds.Tests.Unit.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.Tests.Unit.Healthcheck;

public class HealthcheckEndpointTests(CdsTestFixture appTestFixture) : IClassFixture<CdsTestFixture>
{
    private readonly CdsTestFixture _appTestFixture = appTestFixture;

    [Fact]
    public async Task GivenValidHealthCheckRequest_ShouldSucceed()
    {
        var response = await _appTestFixture.HttpClient.GetAsync("health", TestContext.Current.CancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();
        responseBody.Should().NotBeNullOrEmpty().And.Contain("\"status\": \"Healthy\"");
        responseBody.Should().NotContain("\"status\": \"Degraded\"");
        responseBody.Should().NotContain("\"status\": \"Unhealthy\"");
    }
}