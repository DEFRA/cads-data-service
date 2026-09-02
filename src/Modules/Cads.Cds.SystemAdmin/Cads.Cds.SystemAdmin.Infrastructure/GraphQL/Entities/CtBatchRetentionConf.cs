using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtBatchRetentionConf
{
    public string? BrtItemId { get; set; }

    public decimal? BrtRetentionDays { get; set; }

    public string? BrtDescription { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
