using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class ReportsController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [HttpGet]
    public async Task<IActionResult> GetUserReports([FromQuery] GetUserReportsRequest request)
    {
        var userId = Guid.NewGuid().ToString();

        var query = new GetUserReportsQuery { UserId = userId };

        var result = await _executor.ExecuteQuery(query);

        return Ok(await Task.FromResult(result));
    }
}