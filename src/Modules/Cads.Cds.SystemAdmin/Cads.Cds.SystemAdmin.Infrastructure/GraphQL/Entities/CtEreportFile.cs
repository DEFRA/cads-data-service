using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportFile
{
    public decimal EreId { get; set; }

    public string? EreFileName { get; set; }

    public string? EreFileType { get; set; }

    public decimal? EreLineNumber { get; set; }

    public string? EreRecord { get; set; }

    public DateOnly? EreTimestamp { get; set; }

    public long? TransId { get; set; }
}
