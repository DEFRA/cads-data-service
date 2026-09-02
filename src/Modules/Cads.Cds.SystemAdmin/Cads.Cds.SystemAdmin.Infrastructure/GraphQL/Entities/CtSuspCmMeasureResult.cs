using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspCmMeasureResult
{
    public decimal SmrId { get; set; }

    public decimal? SmrScmId { get; set; }

    public string? SmrMeasureChar { get; set; }

    public decimal? SmrResultNum { get; set; }

    public decimal? SmrMeasureNum { get; set; }

    public string? SmrResultChar { get; set; }

    public string? SmrCurrentStatus { get; set; }

    public DateOnly? SmrCurrentModifiedDate { get; set; }

    public string? SmrCurrentUser { get; set; }

    public decimal? SmrCurrentPid { get; set; }

    public decimal? SmrVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtSuspConditionMarker? SmrScm { get; set; }
}
