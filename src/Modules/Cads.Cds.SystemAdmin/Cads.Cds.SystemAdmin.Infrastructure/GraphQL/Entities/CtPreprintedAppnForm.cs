using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPreprintedAppnForm
{
    public decimal PafId { get; set; }

    public decimal? PafEtgId { get; set; }

    public decimal? PafPpgId { get; set; }

    public string? PafReasonForIssue { get; set; }

    public decimal? PafInterfaceTxnNumber { get; set; }

    public string? PafInterfaceFilename { get; set; }

    public DateOnly? PafDateIssued { get; set; }

    public string? PafCurrentStatus { get; set; }

    public DateOnly? PafCurrentModifiedDate { get; set; }

    public string? PafCurrentUser { get; set; }

    public decimal? PafCurrentPid { get; set; }

    public decimal? PafVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtEartag? PafEtg { get; set; }

    public virtual CtPpafGrouping? PafPpg { get; set; }
}
