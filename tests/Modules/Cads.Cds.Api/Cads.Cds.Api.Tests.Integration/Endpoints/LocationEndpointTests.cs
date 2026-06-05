using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Testing.Support.Utilities.Locations;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using FluentAssertions;

namespace Cads.Cds.Api.Tests.Integration.Endpoints;

[Collection("ApiIntegration"), Trait("Dependence", "testcontainers")]
public class LocationEndpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenInvalidRequest_WhenGetLocationsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(LocationEndpointTestUtilities.InvalidScenario);

        await LocationEndpointTestUtilities.VerifyInvalidScenario(response);
    }

    [Fact]
    public async Task GivenValidRequest_AndCphSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(new LocationQueryBuilder().WithCph(TestLocationConstants.IntegrationTest_CphIdentifiers[0]).Build());

        var locations = await LocationEndpointTestUtilities.VerifyValidScenario(response);

        locations.Count().Should().Be(1);
        locations.Any(x => x.LidFullIdentifier == TestLocationConstants.IntegrationTest_CphIdentifiers[0]).Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndLastModifiedDateSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(new LocationQueryBuilder().WithLastModifiedDate(TestLocationConstants.IntegrationTest_LocationModifiedOn).Build());

        var locations = await LocationEndpointTestUtilities.VerifyValidScenario(response);

        locations.Count().Should().BeGreaterThan(1);
        locations.Any(x => x.LidFullIdentifier == TestLocationConstants.IntegrationTest_CphIdentifiers[0]).Should().BeTrue();
        locations.Any(x => x.LocCurrentModifiedDate == TestLocationConstants.IntegrationTest_LocationModifiedOn).Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndCphSet_AndLastModifiedDateSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(new LocationQueryBuilder()
            .WithCph(TestLocationConstants.IntegrationTest_CphIdentifiers[1])
            .WithLastModifiedDate(TestLocationConstants.IntegrationTest_LocationModifiedOn)
            .Build());

        var locations = await LocationEndpointTestUtilities.VerifyValidScenario(response);

        locations.Count().Should().Be(1);
        locations.Any(x => x.LidFullIdentifier == TestLocationConstants.IntegrationTest_CphIdentifiers[1]).Should().BeTrue();
        locations.Any(x => x.LocCurrentModifiedDate == TestLocationConstants.IntegrationTest_LocationModifiedOn).Should().BeTrue();
    }

    private async Task<HttpResponseMessage> ExecuteTest(string? endpoint)
    {
        var client = apiContainerFixture.CreateBasicClient();

        return await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
    }
}