using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.Generation.Dispatchers;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Generation.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Cads.Cds.SystemAdmin.Generation.Endpoints;

public static class GenerationEndpointExtensions
{
    public static void CreateSystemAdminEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/systemadmin/generation", Generate)
            .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy);

        app.MapGet("/api/v1/systemadmin/generation/scenarios", GetScenarios)
            .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy);
    }

    private static async Task<IResult> Generate(
        [FromBody] CreateGenerationRequest request,
        IScenarioGenerationDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var createGenerationRequestDto = new CreateGenerationRequestDto
        {
            Scenario = request.Scenario,
            RowCount = request.RowCount.GetValueOrDefault()
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
}