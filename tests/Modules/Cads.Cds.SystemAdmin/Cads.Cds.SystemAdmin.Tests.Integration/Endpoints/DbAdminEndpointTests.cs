using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using Cads.Cds.SystemAdmin.Testing.Support.Factories.Authorization;
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
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

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
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

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

        // List commands always return an array; it may be empty if no non-idle sessions matched.
        dto.Result.ValueKind.Should().Be(JsonValueKind.Array);
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenDatabaseSizeRequested_ShouldReturnNonEmptyArrayOrderedBySize()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "database_size",
            args: null,
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={body}");

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("database_size");
        dto.Result.ValueKind.Should().Be(JsonValueKind.Array);
        dto.Result.GetArrayLength().Should().BeGreaterThan(0);

        var sizes = new List<long>();
        foreach (var row in dto.Result.EnumerateArray())
        {
            row.GetProperty("datname").ValueKind.Should().Be(JsonValueKind.String);
            row.GetProperty("size_bytes").ValueKind.Should().Be(JsonValueKind.Number);
            row.GetProperty("size_pretty").ValueKind.Should().Be(JsonValueKind.String);
            sizes.Add(row.GetProperty("size_bytes").GetInt64());
        }

        sizes.Should().BeInDescendingOrder();
        dto.Result.EnumerateArray().Select(r => r.GetProperty("datname").GetString())
            .Should().NotContain("template0").And.NotContain("template1");
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenResetPgStatStatementsRequested_ShouldResetOrReportExtensionNotInstalled()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "reset_pg_stat_statements",
            args: null,
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);
            dto!.Command.Should().Be("reset_pg_stat_statements");
            dto.Result.ValueKind.Should().Be(JsonValueKind.True);
        }
        else
        {
            // The plain postgres test image does not install pg_stat_statements, so the function raises,
            // the same as slow_queries / shared_block_reads.
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError, body);
            body.Should().Contain("pg_stat_statements extension is not installed");
        }
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenPreparedTransactionsRequestedWithNoRows_ShouldReturnEmptyArray()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "prepared_transactions",
            args: null,
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={body}");

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("prepared_transactions");
        dto.Result.ValueKind.Should().Be(JsonValueKind.Array);
        dto.Result.GetArrayLength().Should().Be(0);
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenCancelQueryRequestedWithNonIntegerPid_ShouldReturnBadRequest()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "cancel_query",
            args: new { pid = "not-a-pid" },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await DbAdminTestClient.ReadProblemDetailsAsync(response, TestContext.Current.CancellationToken);
        problemDetails!.Extensions.Should().ContainKey("errors");
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenCancelQueryRequestedForNonExistentPid_ShouldReturnFalse()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

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
    public async Task GivenValidRoleAndScope_WhenCtsImportRunsRequested_ShouldReturnOk()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            client,
            command: "runs",
            args: null,
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={body}");

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("runs");
    }

    [Theory]
    [InlineData("deferred_errors")]
    [InlineData("plan")]
    [InlineData("summary")]
    public async Task GivenValidRoleAndScope_WhenCtsImportRequestedForRunWithNoRows_ShouldReturnOk(string command)
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.ValidUserToken());

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            client,
            command,
            args: new { run_id = long.MaxValue },
            TestContext.Current.CancellationToken);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={body}");

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be(command);
    }

    [Fact(Skip = "LANI-1144")]
    public async Task GivenScopeButNoSuperuserRole_WhenExecuteRequested_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.MissingRoleToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact(Skip = "LANI-1144")]
    public async Task GivenScopeButNoSuperuserRole_WhenCtsImportRequested_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.MissingRoleToken());

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            client,
            command: "runs",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact(Skip = "LANI-1144")]
    public async Task GivenScopeMissing_WhenExecuteRequested_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestDbAdminExecuteTokenFactory.MissingScopeToken());

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}