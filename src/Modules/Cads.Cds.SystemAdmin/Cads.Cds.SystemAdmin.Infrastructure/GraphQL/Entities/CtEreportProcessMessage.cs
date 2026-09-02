using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportProcessMessage
{
    public string? ErqFileType { get; set; }

    public decimal? ErqSleepPeriod { get; set; }

    public decimal? ErqDelayPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
