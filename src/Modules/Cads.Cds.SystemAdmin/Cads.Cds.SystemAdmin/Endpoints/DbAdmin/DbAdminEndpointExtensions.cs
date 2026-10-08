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
        app.MapPost($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/db-admin-execute-command", DbAdminExecuteCommand)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);

        app.MapPost($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/db-admin-cts-import", DbAdminCtsImport)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);

        app.MapGet($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/db-admin-cts-import/runs", GetCtsImportRuns)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);
    }

    private static async Task<GetCtsImportRunsResponse> GetCtsImportRuns(
        IDbAdminExecuteCommandService service,
        HttpContext httpContext,
        ILogger<GetCtsImportRunsResponse> logger,
        CancellationToken cancellationToken)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            var user = httpContext.User.Identity!.Name;
            logger.LogInformation("User {User}: Getting DB Admin cts import runs", user);
        }

        var runs = await service.GetCtsImportRunsAsync(cancellationToken);

        return new GetCtsImportRunsResponse(runs);
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
}