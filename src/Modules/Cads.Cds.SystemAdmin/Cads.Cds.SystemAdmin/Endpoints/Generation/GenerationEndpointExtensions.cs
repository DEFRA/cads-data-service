using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.Generation.Dispatchers;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Endpoints.Generation.Requests;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cts.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsAudit.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Endpoints.Generation;

public static class GenerationEndpointExtensions
{
    public static void CreateGenerationEndpoints(this IEndpointRouteBuilder app)
    {
        const string generationRouteSegment = "/generation";

        app.MapPost(generationRouteSegment, Generate)
            .RequireAuthorization(AuthenticationConstants.DefaultPolicy);

        app.MapGet($"{generationRouteSegment}/scenarios", GetScenarios)
            .RequireAuthorization(AuthenticationConstants.DefaultPolicy);

        app.MapGet($"{generationRouteSegment}/cads/{{tableName}}/dependencies", GetDependencies<CadsSystemAdminDbContext>)
            .RequireAuthorization(AuthenticationConstants.DefaultPolicy);

        app.MapGet($"{generationRouteSegment}/cts/{{tableName}}/dependencies", GetDependencies<CtsSystemAdminDbContext>)
            .RequireAuthorization(AuthenticationConstants.DefaultPolicy);

        app.MapGet($"{generationRouteSegment}/cts-audit/{{tableName}}/dependencies", GetDependencies<CtsAuditSystemAdminDbContext>)
            .RequireAuthorization(AuthenticationConstants.DefaultPolicy);

        app.MapGet($"{generationRouteSegment}/cts-transactions/{{tableName}}/dependencies", GetDependencies<CtsTransactionsSystemAdminDbContext>)
           .RequireAuthorization(AuthenticationConstants.DefaultPolicy);
    }

    private static async Task<IResult> Generate(
        [FromBody] CreateGenerationRequest request,
        IValidator<CreateGenerationRequest> validator,
        IScenarioGenerationDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var createGenerationRequestDto = new CreateGenerationRequestDto
        {
            Scenario = request.Scenario,
            RowCount = request.RowCount,
            BusinessKeys = request.BusinessKeys
        };

        var result = await dispatcher.DispatchAsync(createGenerationRequestDto, cancellationToken);

        var response = new CreateGenerationResponseDto { BusinessKeys = result.BusinessKeys, Content = result.Content, FileName = result.FileName };

        return Results.Ok(response);
    }

    private static async Task<IResult> GetScenarios(IScenarioGenerationDispatcher dispatcher)
    {
        var response = new GetScenariosResponseDto
        {
            Scenarios = dispatcher.Scenarios.Select(i => new ScenarioDto { Name = i.Key, TableName = i.Value.TableName })
        };

        return Results.Ok(response);
    }

    private static async Task<IResult> GetDependencies<T>(ITableDependencyGraph<T> dependencyGraph, [FromRoute] string tableName)
        where T : DbContext
    {
        var response = dependencyGraph.DependenciesOf(tableName);

        return Results.Ok(response);
    }
}