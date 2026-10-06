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
[Route("api/v1/[controller]")]
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

    /// <summary>
    /// Returns a paginated list of animals recorded on a holding (CPH).
    /// </summary>
    /// <remarks>
    /// Results can be filtered by sex and breed, sorted by identifier, birth date, date on CPH, sex or breed code, and paged using
    /// <c>page</c> and <c>pageSize</c>. The response includes <c>totalRecords</c> (the count across
    /// all pages after filtering), so clients can work out how many pages there are.
    /// </remarks>
    /// <param name="request">Holding identifier (CPH).</param>
    /// <param name="filters">Optional filters on animal sex and breed code.</param>
    /// <param name="paging">Page number and page size. The page size is capped at 100.</param>
    /// <param name="sorting">Field to sort by and the sort direction (ascending by default).</param>
    /// <param name="cancellationToken">Token used to cancel the request.</param>
    /// <returns>
    /// <c>200 OK</c> with the animals on the holding (an empty list if nothing matches);
    /// <c>400 Bad Request</c> for invalid parameters; <c>401 Unauthorized</c> if the caller is not authenticated;
    /// </returns>
    [HttpGet("animals")]
    [ProducesResponseType(typeof(AnimalsOnHoldingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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