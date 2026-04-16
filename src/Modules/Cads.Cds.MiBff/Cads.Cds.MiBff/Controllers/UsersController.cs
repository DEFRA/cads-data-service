using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class UsersController(IRequestExecutor executor, IUserContext userContext) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [HttpGet]
    public async Task<IActionResult> GetUserAccess()
    {
        var identifer = userContext.Email ?? Guid.NewGuid().ToString();

        var result = new List<string>();

        return Ok(result);
    }
}
