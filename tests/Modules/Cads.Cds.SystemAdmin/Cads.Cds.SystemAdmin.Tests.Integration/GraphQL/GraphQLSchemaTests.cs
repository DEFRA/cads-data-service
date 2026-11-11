using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cads.Cds.SystemAdmin.Tests.Integration.GraphQL;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class GraphQLSchemaTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    [Theory]
    [InlineData("/graphql/cads")]
    [InlineData("/graphql/cts")]
    [InlineData("/graphql/cts-audit")]
    [InlineData("/graphql/cts-transactions")]
    public async Task GivenSchema_WhenIntrospected_ShouldHaveNoMutationType(string endpoint)
    {
        var response = await _httpClient.PostAsJsonAsync(
            endpoint,
            new { query = "{ __schema { mutationType { name } } }" },
            TestContext.Current.CancellationToken);

        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue($"{(int)response.StatusCode}: {content}");

        var body = JsonNode.Parse(content)!;

        body["errors"].Should().BeNull();
        body["data"]!["__schema"]!["mutationType"].Should().BeNull();
    }
}