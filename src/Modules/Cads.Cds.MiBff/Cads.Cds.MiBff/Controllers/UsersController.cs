using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class UsersController(IUserContext userContext) : ControllerBase
{
    private readonly IUserContext _userContext = userContext;

    [HttpGet]
    public IActionResult GetCurrentUser()
    {
        var currentUser = new
        {
            _userContext.Oid,
            _userContext.Email,
            _userContext.DisplayName,
            _userContext.UserIdentifier
        };

        return Ok(currentUser);
    }

    [HttpGet("claims")]
    public IActionResult GetClaims()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }
}