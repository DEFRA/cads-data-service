using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiEffectiveReportAllPermission
{
    public string? ReportKey { get; set; }

    public string? ExternalSubject { get; set; }

    public string? PermissionKey { get; set; }
}
