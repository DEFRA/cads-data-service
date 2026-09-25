using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class DbAdminEndpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidRoleAndScope_WhenSessionsByStateRequested_ShouldReturnOk()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.DbAdminExecuteToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={body}");

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("sessions_by_state");
        dto.Result.ValueKind.Should().Be(JsonValueKind.Array);
        dto.Result.GetArrayLength().Should().BeGreaterThan(0);

        var first = dto.Result[0];
        first.TryGetProperty("state", out _).Should().BeTrue();
        first.TryGetProperty("sessions", out _).Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenActiveQueriesRequested_ShouldReturnOk()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.DbAdminExecuteToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "active_queries",
            args: null,
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={body}");

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("active_queries");

        // Result may be an array (active sessions found, including this request's own connection)
        // or an empty object {} (jsonb_agg returned NULL because no rows matched).
        dto.Result.ValueKind.Should().BeOneOf(JsonValueKind.Array, JsonValueKind.Object);
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenCancelQueryRequestedForNonExistentPid_ShouldReturnFalse()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.DbAdminExecuteToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "cancel_query",
            args: new { pid = 2147483647 },
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={body}");

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("cancel_query");
        dto.Result.ValueKind.Should().Be(JsonValueKind.False);
    }

    [Fact]
    public async Task GivenRoleMissing_WhenExecuteRequested_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.DbAdminMissingRoleToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenScopeMissing_WhenExecuteRequested_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.DbAdminMissingScopeToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}