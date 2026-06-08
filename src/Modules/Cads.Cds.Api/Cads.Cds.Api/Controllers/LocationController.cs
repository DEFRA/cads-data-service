using Cads.Cds.Api.Application.Queries.Locations;
using Cads.Cds.Api.Controllers.Requests;
using Cads.Cds.Api.Core.DTOs;
using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/[controller]")]
public class LocationController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    /// <summary>
    /// Retrieve a list of available locations.
    /// </summary>
    /// <param name="request">Query parameters to filter locations.</param>
    /// <param name="cancellationToken">A single <see cref="CancellationToken"/>.</param>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<LocationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLocations([FromQuery] GetLocationsRequest request, CancellationToken cancellationToken)
    {
        var result = await _executor.ExecuteQuery(new GetLocationsQuery
        {
            Cph = request.Cph,
            LastModifiedDate = request.LastModifiedDate
        }, cancellationToken);

        return Ok(result);
    }
}