using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
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
    private readonly IRequestExecutor _executor = executor;

    /// <summary>
    /// Retrieve the details of bovine animals matching the supplied identifier.
    /// </summary>
    /// <param name="identifier">The animal identifier (ear tag).</param>
    /// <param name="cancellationToken">A single <see cref="CancellationToken"/>.</param>
    [HttpGet("animals/{identifier}")]
    [ProducesResponseType(typeof(IEnumerable<AnimalDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimalDetails([FromRoute] string identifier, CancellationToken cancellationToken)
    {
        var result = await _executor.ExecuteQuery(new GetAnimalDetailsQuery
        {
            Identifier = identifier
        }, cancellationToken);

        return Ok(result);
    }
}
