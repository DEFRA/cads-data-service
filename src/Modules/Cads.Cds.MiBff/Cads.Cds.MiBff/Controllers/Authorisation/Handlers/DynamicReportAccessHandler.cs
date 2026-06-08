using Cads.Cds.MiBff.Controllers.Authorisation.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Controllers.Authorisation.Handlers;

public class DynamicReportAccessHandler(IServiceProvider serviceProvider) : AuthorizationHandler<DynamicReportAccessRequirement>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DynamicReportAccessRequirement requirement)
    {
        var httpContext = context.Resource switch
        {
            HttpContext ctx => ctx,
            AuthorizationFilterContext filter => filter.HttpContext,
            _ => null
        };

        if (httpContext is null)
            return;

        if (!httpContext.Request.RouteValues.TryGetValue("reportKey", out var keyObj))
            return;

        var reportKey = keyObj?.ToString();
        if (string.IsNullOrWhiteSpace(reportKey))
            return;

        // Re-run authorization using the static policy
        var authorizationService = _serviceProvider.GetRequiredService<IAuthorizationService>();
        var result = await authorizationService.AuthorizeAsync(
            context.User,
            resource: null,
            policyName: $"ReportAccess:{reportKey}");

        if (result.Succeeded)
            context.Succeed(requirement);
    }
}