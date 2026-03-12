using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Queries.Dashboards;
using Cads.Cds.MiBff.Controllers.Requests;
using Cads.Cds.MiBff.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[ApiController]
[Route("api/v1/bff/[controller]")]
public class MiController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ResponseWithMetaData]
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardIndex([FromQuery] GetDashboardsPagedRequest request)
    {
        /// HttpContext.User.Identity.Name;
        var userId = "user"; // TODO get logged in user identity

        var query = QueryFactory.CreatePagedQuery<GetDashboardsQuery, DashboardListingDto>(request);
        query.UserId = userId;

        var result = await _executor.ExecuteQuery(query);

        return Ok(await Task.FromResult(result));
    }
}