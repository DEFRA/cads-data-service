using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Cads.Cds.SystemAdmin.Endpoints.SqsAdmin;

public static class SqsAdminEndpointExtensions
{
    public static void CreateSqsAdminEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/sqs/queues", GetQueues)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);

        app.MapGet($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/sqs/queues/{{queue}}/metrics", GetMetrics)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);

        app.MapPost($"{SysyemAdminEndpointsConstants.ApiRoutePrefix}/sqs/queues/{{queue}}/dlq/replay", ReplayMessagesToQueue)
            .RequireAuthorization(AuthenticationConstants.AadDbAdminExecutePolicy);
    }

    private static async Task<IResult> GetQueues()
    {
        // Implement logic to get SQS queues
        return Results.Ok(new { Queues = new List<string> { "Queue1", "Queue2" } });
    }

    private static async Task<IResult> GetMetrics([FromRoute] string queue)
    {
        // Implement logic to get metrics for the specified SQS queue
        return Results.Ok(new { Queue = queue, Metrics = new { MessagesSent = 100, MessagesReceived = 80 } });
    }

    private static async Task<IResult> ReplayMessagesToQueue([FromRoute] string queue)
    {
        // Implement logic to replay messages to the specified SQS queue
        return Results.Ok(new { Queue = queue, Status = "Messages replayed successfully" });
    }
}