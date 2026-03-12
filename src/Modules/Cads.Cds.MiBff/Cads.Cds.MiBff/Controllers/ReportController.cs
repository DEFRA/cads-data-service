using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.MiBff.Application.Queries.Dashboards;
using Cads.Cds.MiBff.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[ApiController]
[Route("api/v1/bff/[controller]")]
public class ReportController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetUserReportList([FromQuery] GetUserReportListRequest request)
    {
        /// HttpContext.User.Identity.Name;
        var userId = "user"; // TODO get logged in user identity //NOSONAR

        var query = new GetUserReportListQuery { UserId = userId };
        query.UserId = userId;

        var result = await _executor.ExecuteQuery(query);

        return Ok(await Task.FromResult(result));
    }
}