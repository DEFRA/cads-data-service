using Cads.Cds.MiBff.Controllers.Authorisation.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Controllers.Authorisation.Providers;

public class ReportAccessPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _fallback = new(options);

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        => _fallback.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        => _fallback.GetFallbackPolicyAsync();

    /// <summary>
    /// Policy format: "ReportAccess:<reportKey>"
    /// </summary>
    /// <param name="policyName">A single <see cref="string"/> reference.</param>
    /// <returns>A single <see cref="AuthorizationPolicy"/>.</returns>
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("ReportAccess:", StringComparison.OrdinalIgnoreCase))
        {
            var reportKey = policyName["ReportAccess:".Length..];

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new ReportAccessRequirement(reportKey))
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }

        if (policyName == "ReportAccess")
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new DynamicReportAccessRequirement())
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }

        return _fallback.GetPolicyAsync(policyName);
    }
}