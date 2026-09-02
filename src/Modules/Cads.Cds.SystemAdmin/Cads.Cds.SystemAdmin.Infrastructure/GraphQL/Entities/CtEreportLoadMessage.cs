using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportLoadMessage
{
    public string? ErmDirectoryKey { get; set; }

    public string? ErmFileType { get; set; }

    public string? ErmFilePrefix { get; set; }

    public string? ErmFileSuffix { get; set; }

    public decimal? ErmSleepPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
