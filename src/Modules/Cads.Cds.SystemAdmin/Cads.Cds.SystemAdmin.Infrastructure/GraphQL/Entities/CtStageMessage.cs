using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtStageMessage
{
    public string? StmDirectoryKey { get; set; }

    public string? StmFileType { get; set; }

    public string? StmFilePrefix { get; set; }

    public string? StmFileSuffix { get; set; }

    public decimal? StmSleepPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}