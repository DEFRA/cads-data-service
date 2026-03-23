using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class ReportsController(IRequestExecutor executor, IUserContext userContext) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ExcludeFromCodeCoverage]
    [HttpGet]
    public async Task<IActionResult> GetUserReports()
    {
        // TODO: Remove hardcoded email and use user context to get the email of the logged-in user
        var query = new GetUserReportsByEmailQuery { Email = userContext.Email ?? "gary.fletcher@defra.gov.uk" };

        var result = await _executor.ExecuteQuery(query);

        return Ok(await Task.FromResult(result));
    }
}