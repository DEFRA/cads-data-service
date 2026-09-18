using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class MiEffectiveReportAllPermission
{
    public string? ReportKey { get; set; }

    public string? ExternalSubject { get; set; }

    public string? PermissionKey { get; set; }
}