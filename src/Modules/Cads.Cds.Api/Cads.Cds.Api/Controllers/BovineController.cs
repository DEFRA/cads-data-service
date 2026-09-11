using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;
using Cads.Cds.Api.Controllers.Requests;
using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/bovine")]
public class BovineController(IRequestExecutor executor) : ControllerBase
{
    [HttpGet("animals")]
    [ProducesResponseType(typeof(AnimalCollectionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimalsOnCph([FromQuery] GetAnimalsOnCphRequest request, CancellationToken cancellationToken)
    {
        var query = new GetAnimalsOnCph
        {
            Cph = request.Cph ?? string.Empty,
            Q = request.Q,
            Sex = request.Sex,
            DateOnCphFrom = request.DateOnCphFrom
        };

        if (request.HoldingAssociation.HasValue)
            query.HoldingAssociation = request.HoldingAssociation.Value;

        if (request.Status is not null)
            query.Status = request.Status;

        if (request.BreedCode is not null)
            query.BreedCode = request.BreedCode;

        if (request.Page.HasValue)
            query.Page = request.Page.Value;

        if (request.PageSize.HasValue)
            query.PageSize = request.PageSize.Value;

        if (request.OrderBy.HasValue)
            query.OrderBy = request.OrderBy.Value;

        if (request.Direction.HasValue)
            query.Direction = request.Direction.Value;

        var result = await executor.ExecuteQuery(query, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("animals/{identifier}")]
    [ProducesResponseType(typeof(AnimalDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimalDetails([FromRoute] string identifier, CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteQuery(new GetAnimalDetailsByIdentifier
        {
            Identifier = identifier
        }, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}