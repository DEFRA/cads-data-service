using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtStageLock
{
    public string? StlFileType { get; set; }

    public string? StlFileName { get; set; }

    public string? StlProcessed { get; set; }

    public DateOnly? StlTimestamp { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
