using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLabelSummary
{
    public decimal LasId { get; set; }

    public decimal? LasLocIdIdentifying { get; set; }

    public decimal? LasLocIdLabels { get; set; }

    public decimal? LasLabelVersionNumber { get; set; }

    public DateOnly? LasLastSubmittedDate { get; set; }

    public string? LasDefaultLabelType { get; set; }

    public decimal? LasDefaultSheetQuantity { get; set; }

    public string? LasCurrentUser { get; set; }

    public string? LasCurrentStatus { get; set; }

    public DateOnly? LasCurrentModifiedDate { get; set; }

    public decimal? LasCurrentPid { get; set; }

    public decimal? LasVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtLabelRequest> CtLabelRequests { get; set; } = new List<CtLabelRequest>();

    public virtual CtLocation? LasLocIdIdentifyingNavigation { get; set; }

    public virtual CtLocation? LasLocIdLabelsNavigation { get; set; }
}