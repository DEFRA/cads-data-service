using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationRelationship
{
    public decimal LlrId { get; set; }

    public decimal? LlrLocIdParent { get; set; }

    public decimal? LlrLocIdChild { get; set; }

    public DateOnly? LlrEffectiveFromDate { get; set; }

    public string? LlrCessationReason { get; set; }

    public string? LlrComments { get; set; }

    public decimal? LlrLrtId { get; set; }

    public DateOnly? LlrEffectiveToDate { get; set; }

    public string? LlrCurrentStatus { get; set; }

    public DateOnly? LlrCurrentModifiedDate { get; set; }

    public string? LlrCurrentUser { get; set; }

    public decimal? LlrCurrentPid { get; set; }

    public decimal? LlrVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtLocation? LlrLocIdChildNavigation { get; set; }

    public virtual CtLocation? LlrLocIdParentNavigation { get; set; }

    public virtual CtLocationRelType? LlrLrt { get; set; }
}