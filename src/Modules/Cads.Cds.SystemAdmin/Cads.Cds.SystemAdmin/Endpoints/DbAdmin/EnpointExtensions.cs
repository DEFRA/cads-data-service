using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

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
        IDbAdminExecuteCommandService service)
    {
        var result = await service.ExecuteAsync(request.Command, request.Args);

        return new DbAdminExecuteCommandResponse(
            request.Command,
            result.RootElement
        );
    }
}