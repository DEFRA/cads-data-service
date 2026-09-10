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
