using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.StsOrCognitoPolicy)]
[Route("api/v1/[controller]")]
public class FileImportsController : ControllerBase
{
    [HttpGet("{identifier}")]
    public static IResult GetById([FromRoute] string identifier, CancellationToken cancellationToken)
    {
        return Results.Ok(new { Identifier = identifier });
    }

    [HttpGet("scoped/{fileName}")]
    [Authorize(Policy = AuthenticationConstants.StsOrCognitoPolicyFileImports)]
    public static IResult GetByName([FromRoute] string fileName, CancellationToken cancellationToken)
    {
        return Results.Ok(new { FileName = fileName });
    }
}