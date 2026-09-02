using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocTypeRelComb
{
    public decimal LrcId { get; set; }

    public decimal? LrcLtyId1 { get; set; }

    public decimal? LrcLtyId2 { get; set; }

    public decimal? LrcLrtId { get; set; }

    public string? LrcCurrentUser { get; set; }

    public DateOnly? LrcCurrentModifiedDate { get; set; }

    public string? LrcCurrentStatus { get; set; }

    public decimal? LrcCurrentPid { get; set; }

    public decimal? LrcVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtLocationRelType? LrcLrt { get; set; }

    public virtual CtLocationType? LrcLtyId1Navigation { get; set; }

    public virtual CtLocationType? LrcLtyId2Navigation { get; set; }
}
