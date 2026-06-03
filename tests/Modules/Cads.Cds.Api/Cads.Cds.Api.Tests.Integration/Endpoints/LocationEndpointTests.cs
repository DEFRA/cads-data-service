using Cads.Cds.Api.Testing.Support.Utilities.Locations;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

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
        var response = await ExecuteTest(LocationEndpointTestUtilities.ValidScenario_WithCph);

        await LocationEndpointTestUtilities.VerifyValidScenario_WithCph(response);
    }

    [Fact]
    public async Task GivenValidRequest_AndLastModifiedDateSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(LocationEndpointTestUtilities.ValidScenario_WithLastModifiedDate);

        await LocationEndpointTestUtilities.VerifyValidScenario_WithLastModifiedDate(response);
    }

    [Fact]
    public async Task GivenValidRequest_AndCphSet_AndLastModifiedDateSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest(LocationEndpointTestUtilities.ValidScenario_WithCph_AndLastModifiedDate);

        await LocationEndpointTestUtilities.VerifyValidScenario_WithCph_AndLastModifiedDate(response);
    }

    private async Task<HttpResponseMessage> ExecuteTest(string? endpoint)
    {
        var client = apiContainerFixture.CreateBasicClient();

        return await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
    }
}