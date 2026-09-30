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
using Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public class DbAdminExecuteCommandHandlerTests
{
    private readonly Mock<IValidator<DbAdminExecuteCommandRequest>> _executeCommandValidator = new();
    private readonly Mock<ILogger<DbAdminExecuteCommandRequest>> _executeCommandLogger = new();

    private readonly Mock<IValidator<DbAdminCtsImportRequest>> _ctsImportValidator = new();
    private readonly Mock<ILogger<DbAdminCtsImportRequest>> _ctsImportLogger = new();

    private readonly Mock<IDbAdminExecuteCommandService> _service = new();

    public DbAdminExecuteCommandHandlerTests()
    {
        _executeCommandValidator
            .Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<DbAdminExecuteCommandRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _ctsImportValidator
            .Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<DbAdminCtsImportRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(x => x.ExecuteAsync(It.IsAny<DbAdminRequestBaseDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonDocument.Parse("{}"));
    }

    // db-admin-execute-command endpoint

    [Fact]
    public async Task GivenInformationLoggingEnabled_WhenExecuteCommandExecuted_ShouldLogUserAndCommandAndArgs()
    {
        _executeCommandLogger.EnableAllLogLevels();

        var request = new DbAdminExecuteCommandRequest("sessions_by_state", null);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(
            "DbAdminExecuteCommand", request, _executeCommandValidator.Object, _executeCommandLogger.Object, httpContext);

        VerifyLog(_executeCommandLogger, LogLevel.Information, Times.Once(), message =>
            message.Contains("test-db-admin-user") && message.Contains("sessions_by_state"));
    }

    [Fact]
    public async Task GivenInformationLoggingDisabled_WhenExecuteCommandExecuted_ShouldNotLog()
    {
        var request = new DbAdminExecuteCommandRequest("sessions_by_state", null);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(
            "DbAdminExecuteCommand", request, _executeCommandValidator.Object, _executeCommandLogger.Object, httpContext);

        VerifyLog(_executeCommandLogger, LogLevel.Information, Times.Never());
    }

    [Fact]
    public async Task GivenInformationLoggingEnabled_WhenExecuteCommandExecutedWithArgs_ShouldLogRequestArgs()
    {
        _executeCommandLogger.EnableAllLogLevels();

        using var argsDoc = JsonDocument.Parse("""{"pid":123}""");
        var request = new DbAdminExecuteCommandRequest("cancel_query", argsDoc.RootElement);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(
            "DbAdminExecuteCommand", request, _executeCommandValidator.Object, _executeCommandLogger.Object, httpContext);

        VerifyLog(_executeCommandLogger, LogLevel.Information, Times.Once(), message =>
            message.Contains("cancel_query") && message.Contains("123"));
    }

    // db-admin-cts-import endpoint

    [Fact]
    public async Task GivenInformationLoggingEnabled_WhenCtsImportExecuted_ShouldLogUserAndCommandAndArgs()
    {
        _ctsImportLogger.EnableAllLogLevels();

        using var argsDoc = JsonDocument.Parse("""{"run_id":123}""");
        var request = new DbAdminCtsImportRequest("get_cts_parallel_import_summary", argsDoc.RootElement);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(
            "DbAdminCtsImport", request, _ctsImportValidator.Object, _ctsImportLogger.Object, httpContext);

        VerifyLog(_ctsImportLogger, LogLevel.Information, Times.Once(), message =>
            message.Contains("test-db-admin-user") && message.Contains("get_cts_parallel_import_summary"));
    }

    [Fact]
    public async Task GivenInformationLoggingDisabled_WhenCtsImportExecuted_ShouldNotLog()
    {
        using var argsDoc = JsonDocument.Parse("""{"run_id":123}""");
        var request = new DbAdminCtsImportRequest("get_cts_parallel_import_summary", argsDoc.RootElement);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(
            "DbAdminCtsImport", request, _ctsImportValidator.Object, _ctsImportLogger.Object, httpContext);

        VerifyLog(_ctsImportLogger, LogLevel.Information, Times.Never());
    }

    [Fact]
    public async Task GivenInformationLoggingEnabled_WhenCtsImportExecutedWithArgs_ShouldLogRequestArgs()
    {
        _ctsImportLogger.EnableAllLogLevels();

        using var argsDoc = JsonDocument.Parse("""{"run_id":456}""");
        var request = new DbAdminCtsImportRequest("get_cts_parallel_import_plan", argsDoc.RootElement);
        var httpContext = CreateHttpContext("test-db-admin-user");

        await InvokeAsync(
            "DbAdminCtsImport", request, _ctsImportValidator.Object, _ctsImportLogger.Object, httpContext);

        VerifyLog(_ctsImportLogger, LogLevel.Information, Times.Once(), message =>
            message.Contains("get_cts_parallel_import_plan") && message.Contains("456"));
    }

    private async Task<DbAdminCommandResponse> InvokeAsync<TRequest>(
        string methodName,
        TRequest request,
        IValidator<TRequest> validator,
        ILogger<TRequest> logger,
        HttpContext httpContext)
    {
        // EnpointExtensions is a static class, so it can't be used as a generic type argument
        // with MethodInfoUtility.GetPrivateStatic<T> - resolve it directly via typeof() instead.
        // Both db-admin-execute-command and db-admin-cts-import handlers share the same
        // (request, validator, service, httpContext, logger, cancellationToken) parameter shape,
        // so a single generic reflection helper covers both endpoints.
        var method = typeof(DbAdminEndpointExtensions).GetMethod(
            methodName,
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            ?? throw new InvalidOperationException($"Method {methodName} not found");

        var task = (Task<DbAdminCommandResponse>)method.Invoke(
            null,
            [request, validator, _service.Object, httpContext, logger, TestContext.Current.CancellationToken])!;

        return await task;
    }

    private static HttpContext CreateHttpContext(string userName) =>
        new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
                [new Claim(ClaimTypes.Name, userName)],
                authenticationType: "TestAuth"))
        };

    private static void VerifyLog<TCategory>(
        Mock<ILogger<TCategory>> logger,
        LogLevel level,
        Times times,
        Func<string, bool>? messagePredicate = null) =>
        logger.Verify(
            x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, _) => messagePredicate == null || messagePredicate(state.ToString()!)),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
}