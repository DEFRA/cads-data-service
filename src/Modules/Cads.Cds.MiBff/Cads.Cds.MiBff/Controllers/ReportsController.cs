using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class ReportsController(IRequestExecutor executor, IUserContext userContext) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [HttpGet]
    public async Task<IActionResult> GetUserReports()
    {
        var query = new GetUserReportsQuery { Identifier = userContext.Email ?? Guid.NewGuid().ToString() };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [HttpPost("holding_summary")]
    public async Task<IActionResult> GetHoldingSummaryReport(GetHoldingSummaryReportRequest request)
    {
        var query = new GetHoldingSummaryReportQuery();

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}