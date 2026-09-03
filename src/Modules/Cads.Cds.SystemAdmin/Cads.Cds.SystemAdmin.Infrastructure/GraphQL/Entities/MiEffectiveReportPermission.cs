using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiEffectiveReportPermission
{
    public Guid? ReportId { get; set; }

    public string? ReportKey { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public string? DisplayName { get; set; }

    public string? ExternalSubject { get; set; }

    public bool? Granted { get; set; }
}