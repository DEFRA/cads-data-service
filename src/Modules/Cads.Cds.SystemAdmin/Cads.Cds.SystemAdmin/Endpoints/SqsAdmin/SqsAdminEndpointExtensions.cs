using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.SqsAdmin.Services;
using Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;
using Cads.Cds.SystemAdmin.Endpoints.SqsAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.SqsAdmin.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.SystemAdmin.Endpoints.SqsAdmin;

public static class SqsAdminEndpointExtensions
{
    public static void CreateSqsAdminEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/sqs/queues", GetQueues)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);

        app.MapGet($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/sqs/queues/{{queue}}/metrics", GetMetrics)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);

        app.MapGet($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/sqs/queues/{{queue}}/messages", GetMessages)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);

        app.MapPost($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/sqs/queues/{{queue}}/dlq/replay", ReplayMessagesToQueue)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);
    }

    private static async Task<IResult> GetQueues(
        ISqsAdminService service,
        CancellationToken cancellationToken)
    {
        var queues = await service.GetQueuesAsync(cancellationToken);
        return Results.Ok(new GetQueuesResponse(queues));
    }

    private static async Task<IResult> GetMetrics(
        [FromRoute] string queue,
        ISqsAdminService service,
        CancellationToken cancellationToken)
    {
        var metrics = await service.GetMetricsAsync(queue, cancellationToken);
        return Results.Ok(new GetQueueMetricsResponse(queue, metrics));
    }

    private static async Task<IResult> GetMessages(
        [FromRoute] string queue,
        [FromQuery] int maxMessages,
        [FromQuery] int visibilityTimeout,
        ISqsAdminService service,
        CancellationToken cancellationToken)
    {
        // Messages are always returned non-destructively (peek); the caller-supplied
        // visibilityTimeout controls how long they remain hidden before becoming visible again.
        var request = new PeekMessagesRequestDto(
            queue,
            maxMessages > 0 ? maxMessages : 10,
            visibilityTimeout >= 0 ? visibilityTimeout : 30);

        var messages = await service.PeekMessagesAsync(request, cancellationToken);
        return Results.Ok(new GetQueueMessagesResponse(queue, messages));
    }

    private static async Task<IResult> ReplayMessagesToQueue(
        [FromRoute] string queue,
        ReplayDlqRequest request,
        IValidator<ReplayDlqRequest> validator,
        ISqsAdminService service,
        HttpContext httpContext,
        ILogger<ReplayDlqRequest> logger,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        if (logger.IsEnabled(LogLevel.Information))
        {
            var user = httpContext.User.Identity!.Name;
            logger.LogInformation(
                "User {User}: Replaying DLQ messages for queue {Queue} with BatchSize: {BatchSize}",
                user, queue, request.BatchSize);
        }

        var result = await service.ReplayDlqAsync(
            new ReplayDlqRequestDto(queue, request.BatchSize ?? 0),
            cancellationToken);

        return Results.Ok(new ReplayDlqResponse(queue, result.Moved, result.Failed, result.Errors));
    }
}

