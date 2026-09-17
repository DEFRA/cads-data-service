using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;
using Cads.Cds.Api.Controllers.Adapters.Bovine;
using Cads.Cds.Api.Controllers.Requests.Bovine;
using Cads.Cds.Api.Controllers.Requests.Common;
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
    [HttpGet("animals/{identifier}")]
    [ProducesResponseType(typeof(AnimalDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimalDetailsByIdentifier([FromRoute] string identifier, CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteQuery(new GetAnimalDetailsByIdentifier
        {
            Identifier = identifier
        }, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("animals")]
    [ProducesResponseType(typeof(AnimalsOnHoldingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimalsOnHolding(
        [FromQuery] GetAnimalsOnHoldingRequest request,
        [FromQuery] AnimalsOnHoldingFilters filters,
        [FromQuery] PagingOptionsRequest paging,
        [FromQuery] SortingOptionsRequest<AnimalsOnHoldingOrderBy> sorting,
        CancellationToken cancellationToken)
    {
        var query = AnimalsOnHoldingRequestAdapter.ToQuery(request, filters, paging, sorting);

        var result = await executor.ExecuteQuery(query, cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}