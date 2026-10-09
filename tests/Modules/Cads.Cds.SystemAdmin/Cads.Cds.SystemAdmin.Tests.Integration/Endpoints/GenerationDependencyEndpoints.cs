using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class GenerationDependencyEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    [Fact]
    public async Task GivenValidGenerationDependencyRequest_For_cts_schema_ctLocations_ShouldSucceed()
    {
        var response = await GenerationTestClient.GetDependenciesAsync(_httpClient,
            "cts",
            "ct_locations",
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await GenerationTestClient.ReadDtoAsync<List<string>>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenValidGenerationDependencyRequest_For_ctsaudit_schema_ctLocations_ShouldSucceed()
    {
        var response = await GenerationTestClient.GetDependenciesAsync(_httpClient,
            "cts-audit",
            "ct_locations",
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await GenerationTestClient.ReadDtoAsync<List<string>>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenValidGenerationDependencyRequest_For_ctstransactions_schema_ctLocations_ShouldSucceed()
    {
        var response = await GenerationTestClient.GetDependenciesAsync(_httpClient,
            "cts-transactions",
            "ct_locations",
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await GenerationTestClient.ReadDtoAsync<List<string>>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().BeEmpty();
    }
}