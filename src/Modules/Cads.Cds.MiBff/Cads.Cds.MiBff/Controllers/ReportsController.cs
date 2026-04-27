using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
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
    private readonly IUserContext _userContext = userContext;

    [HttpGet]
    public async Task<IActionResult> GetUserReports()
    {
        var query = new GetUserReportsQuery { Identifier = _userContext.UserIdentifier ?? Guid.NewGuid().ToString() };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
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

    [HttpGet("{reportKey}/permissions")]
    public async Task<IActionResult> GetUserReportPermissions([FromRoute] string reportKey)
    {
        var query = new GetUserReportPermissionsQuery { ExternalSubject = _userContext.UserIdentifier ?? Guid.NewGuid().ToString(), ReportKey = reportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:animal_summary")]
    [HttpPost("animal_summary")]
    public async Task<IActionResult> GetAnimalSummaryReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:cohort_tracing")]
    [HttpPost("cohort_tracing")]
    public async Task<IActionResult> GetCohortTracingReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:holding_summary")]
    [HttpPost("holding_summary")]
    public async Task<IActionResult> GetHoldingSummaryReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:journey_by_haulier")]
    [HttpPost("journey_by_haulier")]
    public async Task<IActionResult> GetJourneyByHaulierReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:movements_all_holdings")]
    [HttpPost("movements_all_holdings")]
    public async Task<IActionResult> GetMovementsAllHoldingsReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:movement_summary_holding")]
    [HttpPost("movement_summary_holding")]
    public async Task<IActionResult> GetMovementSummaryHoldingReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:scrapie_flock_scheme_audit")]
    [HttpPost("scrapie_flock_scheme_audit")]
    public async Task<IActionResult> GetScrapieFlockSchemeAuditReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:sheep_goat_inspections")]
    [HttpPost("sheep_goat_inspections")]
    public async Task<IActionResult> GetSheepGoatInspectionsReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:unregistered_herds_flocks")]
    [HttpPost("unregistered_herds_flocks")]
    public async Task<IActionResult> GetUnregisteredHerdsFlocksReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:zonal_movements_summary")]
    [HttpPost("zonal_movements_summary")]
    public async Task<IActionResult> GetZonalMovementsSummaryReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}