using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspMovementError
{
    public decimal? SmeSmoId { get; set; }

    public decimal SmeId { get; set; }

    public string? SmeAttributeName { get; set; }

    public string? SmeErrorCode { get; set; }

    public string? SmeCurrentUser { get; set; }

    public DateOnly? SmeCurrentModifiedDate { get; set; }

    public string? SmeCurrentStatus { get; set; }

    public decimal? SmeCurrentPid { get; set; }

    public decimal? SmeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtSuspendedMovement? SmeSmo { get; set; }
}
