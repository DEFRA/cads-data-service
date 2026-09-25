using System.Runtime.CompilerServices;
using System.Security.Claims;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin;

public static class EnpointExtensions
{
    public static void CreateDbAdminEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/systemadmin/db-admin-execute-command", DbAdminExecuteCommand)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);
    }

    private static async Task<DbAdminExecuteCommandResponse> DbAdminExecuteCommand(
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
        var result = await service.ExecuteAsync(request.Command, request.Args, cancellationToken);

        return new DbAdminExecuteCommandResponse(
            request.Command,
            result.RootElement
        );
    }
}