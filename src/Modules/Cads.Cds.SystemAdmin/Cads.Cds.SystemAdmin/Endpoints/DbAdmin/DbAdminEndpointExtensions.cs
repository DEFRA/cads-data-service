using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin;

public static class DbAdminEndpointExtensions
{
    public static void CreateDbAdminEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost($"{SystemAdminEndpointsConstants.ApiRoutePrefix}/db-admin-execute-command", DbAdminExecuteCommand)
            .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy); // Revert to AadDbAdminExecutePolicy once we have scopes. Revert [Fact(Skip = "LANI-1144")] tests.

        app.MapPost($"{SystemAdminEndpointsConstants.ApiRoutePrefix}/db-admin-cts-import", DbAdminCtsImport)
            .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy); // Revert to AadDbAdminExecutePolicy once we have scopes. Revert [Fact(Skip = "LANI-1144")] tests.

        app.MapPost($"{SystemAdminEndpointsConstants.ApiRoutePrefix}/db-admin-prepare-cts-import", DbAdminPrepareCtsImport)
            .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy); // Revert to AadDbAdminExecutePolicy once we have scopes. Revert [Fact(Skip = "LANI-1144")] tests.
    }

    private static async Task<DbAdminCommandResponse> DbAdminExecuteCommand(
        DbAdminExecuteCommandRequest request,
        IValidator<DbAdminExecuteCommandRequest> validator,
        IDbAdminExecuteCommandService service,
        HttpContext httpContext,
        ILogger<DbAdminExecuteCommandRequest> logger,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
        {
            var user = httpContext.User.Identity!.Name;
            logger.LogInformation("User {User}: Executing DB Admin command: {Command} with args: {Args}", user, request.Command, request.Args);
        }

        var requestDto = new DbAdminExecuteCommandRequestDto(request.Command, request.Args);
        var result = await service.ExecuteAsync(requestDto, cancellationToken);

        return new DbAdminCommandResponse(
            request.Command,
            result.RootElement
        );
    }

    private static async Task<DbAdminCommandResponse> DbAdminCtsImport(
        DbAdminCtsImportRequest request,
        IValidator<DbAdminCtsImportRequest> validator,
        IDbAdminExecuteCommandService service,
        HttpContext httpContext,
        ILogger<DbAdminCtsImportRequest> logger,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
        {
            var user = httpContext.User.Identity!.Name;
            logger.LogInformation("User {User}: Executing DB Admin cts import: {Command} with run id: {Args}", user, request.Command, request.Args);
        }
        var requestDto = new DbAdminCtsImportRequestDto(request.Command, request.Args);
        var result = await service.ExecuteAsync(requestDto, cancellationToken);

        return new DbAdminCommandResponse(
            request.Command,
            result.RootElement
        );
    }

    private static IResult DbAdminPrepareCtsImport(
        ICtsImportPrepareJobQueue queue,
        HttpContext httpContext,
        ILogger<DbAdminCtsImportRequest> logger)
    {
        // Cognito client-credentials access tokens have no name claim, so identify the caller by client_id.
        var user = httpContext.User.FindFirst("client_id")?.Value
                   ?? httpContext.User.FindFirst("sub")?.Value;

        if (!queue.TryEnqueue(user))
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("User {User}: DB Admin prepare cts import rejected, a prepare is already queued or running", user);
            }

            return Results.Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "CTS import prepare already in progress",
                detail: "A CTS import prepare is already queued or running. Check the 'runs' command on db-admin-cts-import for the outcome.");
        }

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("User {User}: Queued DB Admin prepare cts import", user);
        }

        return Results.Accepted(value: new DbAdminPrepareCtsImportResponse(
            "Accepted",
            "CTS import prepare started. This can take 20-30 minutes. Poll the 'runs' command on db-admin-cts-import; the new run appears with status 'ready' when planning completes."));
    }
}