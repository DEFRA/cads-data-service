using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public class DbAdminExecuteCommandHandlerTests
{
    private readonly Mock<IValidator<DbAdminExecuteCommandRequest>> _validator = new();
    private readonly Mock<IDbAdminExecuteCommandService> _service = new();
    private readonly Mock<ILogger<DbAdminExecuteCommandRequest>> _logger = new();

    public DbAdminExecuteCommandHandlerTests()
    {
        _validator
            .Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<DbAdminExecuteCommandRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<JsonElement?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("{}"));
    }

    [Fact]
    public async Task GivenInformationLoggingEnabled_WhenCommandExecuted_ShouldLogUserAndCommandAndArgs()
    {
        _logger.EnableAllLogLevels();

        var request = new DbAdminExecuteCommandRequest("sessions_by_state", null);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(request, httpContext);

        VerifyLog(LogLevel.Information, Times.Once(), message =>
            message.Contains("test-db-admin-user") && message.Contains("sessions_by_state"));
    }

    [Fact]
    public async Task GivenInformationLoggingDisabled_WhenCommandExecuted_ShouldNotLog()
    {
        var request = new DbAdminExecuteCommandRequest("sessions_by_state", null);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(request, httpContext);

        VerifyLog(LogLevel.Information, Times.Never());
    }

    [Fact]
    public async Task GivenInformationLoggingEnabled_WhenCommandExecutedWithArgs_ShouldLogRequestArgs()
    {
        _logger.EnableAllLogLevels();

        using var argsDoc = JsonDocument.Parse("""{"pid":123}""");
        var request = new DbAdminExecuteCommandRequest("cancel_query", argsDoc.RootElement);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(request, httpContext);

        VerifyLog(LogLevel.Information, Times.Once(), message =>
            message.Contains("cancel_query") && message.Contains("123"));
    }

    private async Task<DbAdminExecuteCommandResponse> InvokeAsync(DbAdminExecuteCommandRequest request, HttpContext httpContext)
    {
        // EnpointExtensions is a static class, so it can't be used as a generic type argument
        // with MethodInfoUtility.GetPrivateStatic<T> - resolve it directly via typeof() instead.
        var method = typeof(EnpointExtensions).GetMethod(
            "DbAdminExecuteCommand",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            ?? throw new InvalidOperationException("Method DbAdminExecuteCommand not found");

        var task = (Task<DbAdminExecuteCommandResponse>)method.Invoke(
            null,
            [request, _validator.Object, _service.Object, httpContext, _logger.Object, TestContext.Current.CancellationToken])!;

        return await task;
    }

    private static HttpContext CreateHttpContext(string userName) =>
        new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
                [new Claim(ClaimTypes.Name, userName)],
                authenticationType: "TestAuth"))
        };

    private void VerifyLog(LogLevel level, Times times, Func<string, bool>? messagePredicate = null) =>
        _logger.Verify(
            x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, _) => messagePredicate == null || messagePredicate(state.ToString()!)),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
}