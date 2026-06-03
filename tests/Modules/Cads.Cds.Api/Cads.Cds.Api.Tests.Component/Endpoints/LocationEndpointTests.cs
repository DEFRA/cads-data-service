using Cads.Cds.Api.Testing.Support.Utilities.Locations;
using Cads.Cds.Api.Tests.Component.TestFixtures;

namespace Cads.Cds.Api.Tests.Component.Endpoints;

public class LocationEndpointTests(ApiTestFixture testFixture) : IClassFixture<ApiTestFixture>
{
    private readonly ApiTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenInvalidRequest_WhenGetLocationsRequested_ShouldReturnBadRequest()
    {
        var response = await _testFixture.HttpClient.GetAsync(LocationEndpointTestUtilities.InvalidScenario, TestContext.Current.CancellationToken);

        await LocationEndpointTestUtilities.VerifyInvalidScenario(response);
    }

    [Fact]
    public async Task GivenValidRequest_AndCphSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await _testFixture.HttpClient.GetAsync(LocationEndpointTestUtilities.ValidScenario_WithCph, TestContext.Current.CancellationToken);

        await LocationEndpointTestUtilities.VerifyValidScenario_WithCph(response);
    }

    [Fact]
    public async Task GivenValidRequest_AndLastModifiedDateSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await _testFixture.HttpClient.GetAsync(LocationEndpointTestUtilities.ValidScenario_WithLastModifiedDate, TestContext.Current.CancellationToken);

        await LocationEndpointTestUtilities.VerifyValidScenario_WithLastModifiedDate(response);
    }

    [Fact]
    public async Task GivenValidRequest_AndCphSet_AndLastModifiedDateSet_WhenGetLocationsRequested_ShouldSucceed()
    {
        var response = await _testFixture.HttpClient.GetAsync(LocationEndpointTestUtilities.ValidScenario_WithCph_AndLastModifiedDate, TestContext.Current.CancellationToken);

        await LocationEndpointTestUtilities.VerifyValidScenario_WithCph_AndLastModifiedDate(response);
    }
}