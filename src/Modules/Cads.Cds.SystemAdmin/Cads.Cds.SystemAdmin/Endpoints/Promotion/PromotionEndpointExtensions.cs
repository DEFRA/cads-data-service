using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Identity;
using Cads.Cds.SystemAdmin.Application.Promotion.Channels;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.Promotion.Requests;
using Cads.Cds.SystemAdmin.Endpoints.Promotion.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.SystemAdmin.Endpoints.Promotion;

public static class PromotionEndpointExtensions
{
    /// <summary>
    /// Special endpoint to trigger a data import plan using cron workers.
    /// </summary>
    /// <param name="app"></param>
    public static void CreatePromotionEndpoints(this IEndpointRouteBuilder app)
    {
        const string promoteRouteSegment = "/promote";

        app.MapPost($"{promoteRouteSegment}/{{type}}", PromoteImport)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);
    }

    private static IResult PromoteImport(
        [FromRoute] PromotionType type,
        [FromBody] PromoteImportRequest request,
        IPromotionJobQueue queue,
        HttpContext httpContext,
        ILogger<DbAdminCtsImportRequest> logger)
    {
        var userName = httpContext.User.GetDisplayName();

        if (!queue.TryEnqueue(type, request.DeleteSource, userName))
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("User {User}: Promote import rejected, a plan is already queued or running", userName);
            }

            return Results.Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Import plan already in progress",
                detail: "An import plan is already queued or running. Check the 'runs' command on 'db-admin-cts-import' for the outcome.");
        }

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("User {User}: Queued import promotion", userName);
        }

        return Results.Accepted(value: new PromoteImportResponse(
            "Accepted",
            "Import promotion started. This can take 20-30 minutes. Poll the 'runs' command on 'db-admin-cts-import'; the new run appears with status 'ready' when planning completes."));
    }
}
