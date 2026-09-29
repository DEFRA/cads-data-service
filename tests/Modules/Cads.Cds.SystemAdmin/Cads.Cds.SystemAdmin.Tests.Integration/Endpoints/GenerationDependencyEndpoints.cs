using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class GenerationDependencyEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    private readonly PostgresDb _postgresDb = new(apiContainerFixture.PostgresFixture.HostConnectionString);

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
}