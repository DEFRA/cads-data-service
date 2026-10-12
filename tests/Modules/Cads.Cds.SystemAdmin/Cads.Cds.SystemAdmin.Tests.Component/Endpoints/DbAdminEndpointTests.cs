using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public class DbAdminEndpointTests(SystemAdminTestFixture testFixture) : IClassFixture<SystemAdminTestFixture>
{
    private readonly SystemAdminTestFixture _testFixture = testFixture;
    private HttpClient _httpClient => _testFixture.HttpClient;

    // Request/response model binding

    [Fact]
    public async Task GivenValidCommand_WhenExecuteRequested_ShouldReturnCommandAndResult()
    {
        var expectedResult = JsonDocument.Parse("""{"state":"active","sessions":1}""");

        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminExecuteCommandRequestDto>(r => r.Command == "sessions_by_state"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("sessions_by_state");
        dto.Result.GetProperty("sessions").GetInt32().Should().Be(1);
    }

    [Fact]
    public async Task GivenMissingCommand_WhenExecuteRequested_ShouldReturnBadRequest()
    {
        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await DbAdminTestClient.ReadProblemDetailsAsync(response, TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails!.Extensions.Should().ContainKey("errors");
    }

    [Fact]
    public async Task GivenUnrecognisedCommand_WhenExecuteRequested_ShouldReturnBadRequest()
    {
        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "not_a_real_command",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenArgsSupplied_WhenExecuteRequested_ShouldBindAndReturnResult()
    {
        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminExecuteCommandRequestDto>(r =>
                    r.Command == "cancel_query"
                    && r.Args.HasValue
                    && r.Args.Value.GetProperty("pid").GetInt32() == 12345),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("true"));

        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "cancel_query",
            args: new { pid = 12345 },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);
        dto!.Command.Should().Be("cancel_query");
        dto.Result.GetBoolean().Should().BeTrue();
    }

    [Theory]
    [InlineData("cancel_query", null)]
    [InlineData("cancel_query", """{}""")]
    [InlineData("cancel_query", """{"pid":"12345"}""")]
    [InlineData("cancel_query", """{"pid":12.5}""")]
    [InlineData("cancel_query", """{"pid":0}""")]
    [InlineData("terminate_query", """{"pid":-1}""")]
    [InlineData("terminate_query", """{"pid":2147483648}""")]
    [InlineData("terminate_query", null)]
    public async Task GivenPidCommandWithMissingOrInvalidPid_WhenExecuteRequested_ShouldReturnBadRequestWithArgsError(
        string command, string? argsJson)
    {
        object? args = argsJson is null ? null : JsonDocument.Parse(argsJson).RootElement.Clone();

        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command,
            args,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        using var problem = JsonDocument.Parse(body);

        problem.RootElement.GetProperty("status").GetInt32().Should().Be(400);
        problem.RootElement.GetProperty("title").GetString().Should().Be("Validation errors");
        problem.RootElement.GetProperty("errors").GetProperty("Args")[0].GetString()
            .Should().Be("Args must contain an integer 'pid' property between 1 and 2147483647.");
    }

    [Fact]
    public async Task GivenDatabaseSizeCommand_WhenExecuteRequested_ShouldReturnCommandAndResult()
    {
        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminExecuteCommandRequestDto>(r => r.Command == "database_size"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("""[{"datname":"cads","size_bytes":8192,"size_pretty":"8192 bytes"}]"""));

        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "database_size",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto!.Command.Should().Be("database_size");
        dto.Result[0].GetProperty("size_bytes").GetInt64().Should().Be(8192);
    }

    [Fact]
    public async Task GivenResetPgStatStatementsCommand_WhenExecuteRequested_ShouldReturnCommandAndResult()
    {
        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminExecuteCommandRequestDto>(r => r.Command == "reset_pg_stat_statements"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("true"));

        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "reset_pg_stat_statements",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto!.Command.Should().Be("reset_pg_stat_statements");
        dto.Result.GetBoolean().Should().BeTrue();
    }

    // Authorization policy behaviour

    [Fact]
    public async Task GivenNoToken_WhenExecuteRequested_ShouldReturnUnauthorized()
    {
        var client = _testFixture.Factory.CreateClient();

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenApiKeyCredentials_WhenExecuteRequested_ShouldReturnUnauthorized()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenScopeClaimMissing_WhenExecuteRequested_ShouldReturnForbidden()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingDbAdminScope);

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenScopeButSuperuserRoleMissing_WhenExecuteRequested_ShouldReturnForbidden()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingSuperuserRole);

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenValidRoleAndScope_WhenExecuteRequested_ShouldReturnOk()
    {
        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminExecuteCommandRequestDto>(r => r.Command == "sessions_by_state"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("[]"));

        // _httpClient already carries the default fake JWT, which includes both the role and scope claims
        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // Service layer invocation

    [Fact]
    public async Task GivenValidRequest_WhenExecuteRequested_ShouldInvokeServiceOnceWithCommandAndArgs()
    {
        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminExecuteCommandRequestDto>(r => r.Command == "active_queries"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("[]"));

        var response = await DbAdminTestClient.ExecuteAsync(
            _httpClient,
            command: "active_queries",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _testFixture.Factory.DbAdminExecuteCommandServiceMock.Verify(x => x.ExecuteAsync(
            It.Is<DbAdminExecuteCommandRequestDto>(r => r.Command == "active_queries" && r.Args == null),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GivenDbAdminEndpointsDisabled_WhenExecuteRequested_ShouldReturnNotFound()
    {
        await using var factory = new SystemAdminWebApplicationFactory(
            configOverrides: new Dictionary<string, string?>
            {
                ["Modules:SystemAdmin:EnableAdminEndpoints"] = "false"
            },
            useFakeAuth: true);

        var client = factory.CreateClient();
        client.AddJwt();

        var response = await DbAdminTestClient.ExecuteAsync(
            client,
            command: "sessions_by_state",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // CTS import: Request/response model binding

    [Fact]
    public async Task GivenValidCommand_WhenCtsImportRequested_ShouldReturnCommandAndResult()
    {
        var expectedResult = JsonDocument.Parse("""[{"status":"complete","count":3}]""");

        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminCtsImportRequestDto>(r => r.Command == "summary"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "summary",
            args: new { run_id = 123L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await DbAdminTestClient.ReadDtoAsync(response, TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.Command.Should().Be("summary");
        dto.Result[0].GetProperty("count").GetInt32().Should().Be(3);
    }

    [Fact]
    public async Task GivenMissingCommand_WhenCtsImportRequested_ShouldReturnBadRequest()
    {
        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "",
            args: new { run_id = 123L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await DbAdminTestClient.ReadProblemDetailsAsync(response, TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails!.Extensions.Should().ContainKey("errors");
    }

    [Fact]
    public async Task GivenUnrecognisedCommand_WhenCtsImportRequested_ShouldReturnBadRequest()
    {
        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "not_a_real_command",
            args: new { run_id = 123L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenArgsMissing_WhenCtsImportRequested_ShouldReturnBadRequest()
    {
        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "plan",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenRunIdMissingFromArgs_WhenCtsImportRequested_ShouldReturnBadRequest()
    {
        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "plan",
            args: new { some_other_field = 1 },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenRunIdNotNumeric_WhenCtsImportRequested_ShouldReturnBadRequest()
    {
        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "plan",
            args: new { run_id = "not-a-number" },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    // CTS import: Authorization policy behaviour

    [Fact]
    public async Task GivenNoToken_WhenCtsImportRequested_ShouldReturnUnauthorized()
    {
        var client = _testFixture.Factory.CreateClient();

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            client,
            command: "summary",
            args: new { run_id = 123L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenScopeClaimMissing_WhenCtsImportRequested_ShouldReturnForbidden()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingDbAdminScope);

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            client,
            command: "summary",
            args: new { run_id = 123L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenScopeButSuperuserRoleMissing_WhenCtsImportRequested_ShouldReturnForbidden()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingSuperuserRole);

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            client,
            command: "summary",
            args: new { run_id = 123L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    // CTS import: Service layer invocation

    [Fact]
    public async Task GivenValidRequest_WhenCtsImportRequested_ShouldInvokeServiceOnceWithCommandAndArgs()
    {
        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminCtsImportRequestDto>(r => r.Command == "deferred_errors"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("[]"));

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "deferred_errors",
            args: new { run_id = 456L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _testFixture.Factory.DbAdminExecuteCommandServiceMock.Verify(x => x.ExecuteAsync(
            It.Is<DbAdminCtsImportRequestDto>(r =>
                r.Command == "deferred_errors"
                && r.Args.HasValue
                && r.Args.Value.GetProperty("run_id").GetInt64() == 456L),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GivenDbAdminEndpointsDisabled_WhenCtsImportRequested_ShouldReturnNotFound()
    {
        await using var factory = new SystemAdminWebApplicationFactory(
            configOverrides: new Dictionary<string, string?>
            {
                ["Modules:SystemAdmin:EnableAdminEndpoints"] = "false"
            },
            useFakeAuth: true);

        var client = factory.CreateClient();
        client.AddJwt();

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            client,
            command: "summary",
            args: new { run_id = 123L },
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // CTS import: runs command

    [Fact]
    public async Task GivenRunsCommandWithoutArgs_WhenCtsImportRequested_ShouldInvokeServiceAndReturnOk()
    {
        _testFixture.Factory.DbAdminExecuteCommandServiceMock
            .Setup(x => x.ExecuteAsync(
                It.Is<DbAdminCtsImportRequestDto>(r => r.Command == "runs"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("[]"));

        var response = await DbAdminTestClient.ExecuteCtsImportAsync(
            _httpClient,
            command: "runs",
            args: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _testFixture.Factory.DbAdminExecuteCommandServiceMock.Verify(x => x.ExecuteAsync(
            It.Is<DbAdminCtsImportRequestDto>(r => r.Command == "runs"),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}