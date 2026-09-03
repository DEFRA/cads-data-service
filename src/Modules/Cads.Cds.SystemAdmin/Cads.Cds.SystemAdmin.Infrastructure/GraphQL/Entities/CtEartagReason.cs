using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagReason
{
    public decimal EtrId { get; set; }

    public string? EtrEartagReasonCode { get; set; }

    public string? EtrReasonCodeType { get; set; }

    public string? EtrShortDescription { get; set; }

    public string? EtrLongDescription { get; set; }

    public string? EtrCurrentStatus { get; set; }

    public string? EtrCurrentUser { get; set; }

    public DateOnly? EtrCurrentModifiedDate { get; set; }

    public decimal? EtrCurrentPid { get; set; }

    public decimal? EtrVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtEartagReasonFlag> CtEartagReasonFlags { get; set; } = new List<CtEartagReasonFlag>();
}