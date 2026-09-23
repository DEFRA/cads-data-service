using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.StsOrCognitoPolicy)]
[Route("api/v1/imports")]
public class ImportController : ControllerBase
{
    [HttpGet("{identifier}")]
    public async Task<IActionResult> GetById([FromRoute] string identifier, CancellationToken cancellationToken)
    {
        return Ok(new { Identifier = identifier });
    }

    [HttpGet("scoped/{fileName}")]
    [Authorize(Policy = AuthenticationConstants.StsOrCognitoPolicyImports)]
    public async Task<IActionResult> GetByName([FromRoute] string fileName, CancellationToken cancellationToken)
    {
        return Ok(new { FileName = fileName });
    }
}