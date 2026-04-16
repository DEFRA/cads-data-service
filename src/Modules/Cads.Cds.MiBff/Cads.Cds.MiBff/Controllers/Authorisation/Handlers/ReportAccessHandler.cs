using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.MiBff.Controllers.Authorisation.Requirements;
using Cads.Cds.MiBff.Core.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Cads.Cds.MiBff.Controllers.Authorisation.Handlers;

public class ReportAccessHandler(IReportService reportService,
    IUserContext userContext,
    IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<ReportAccessRequirement>
{
    private readonly IReportService _reportService = reportService;
    private readonly IUserContext _userContext = userContext;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ReportAccessRequirement requirement)
    {
        var email = _userContext.Email;

        if (email is null)
            return;

        var ct = _httpContextAccessor.HttpContext?.RequestAborted
                 ?? CancellationToken.None;

        var granted = await _reportService.HasReportAccessAsync(
            email,
            requirement.ReportKey,
            ct);

        if (granted)
            context.Succeed(requirement);
    }
}