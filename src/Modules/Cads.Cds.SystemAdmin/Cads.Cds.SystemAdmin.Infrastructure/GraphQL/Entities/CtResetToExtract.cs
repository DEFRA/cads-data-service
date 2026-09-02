using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtResetToExtract
{
    public decimal RteId { get; set; }

    public string? RteTableName { get; set; }

    public string? RteStatus { get; set; }

    public decimal? RteBatch { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
