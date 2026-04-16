using Microsoft.AspNetCore.Authorization;

namespace Cads.Cds.MiBff.Controllers.Authorisation.Requirements;

public class ReportAccessRequirement(string reportKey) : IAuthorizationRequirement
{
    public string ReportKey { get; } = reportKey;
}
