using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Services.Reports;
using Microsoft.AspNetCore.Authorization;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class ReportsController(IRequestExecutor executor, IUserContext userContext, IReportGenerationService irs) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ExcludeFromCodeCoverage]
    [HttpGet]
    public async Task<IActionResult> GetUserReports()
    {
        var query = new GetUserReportsByEmailQuery { Email = userContext.Email! };

        var result = await _executor.ExecuteQuery(query);

        return Ok(await Task.FromResult(result));
    }
    
    [ExcludeFromCodeCoverage]
    [HttpPost("cattle_registrations")]
    public async Task<IActionResult> GetCattleRegistrations()
    {
        var result = await irs.GetCattleRegistrations(new DateTime(), new DateTime());
        result.Position = 0;

        return File(fileContents: result.ToArray(),
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "cattle_registrations.xlsx");
    }
}