using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtParamGroup
{
    public decimal? PgpVersion { get; set; }

    public decimal PgpId { get; set; }

    public string? PgpParam { get; set; }

    public string? PgpGroupValue { get; set; }

    public decimal? PgpPhdId { get; set; }

    public string? PgpShortDesc { get; set; }

    public string? PgpLongDesc { get; set; }

    public string? PgpCurrentUser { get; set; }

    public string? PgpCurrentStatus { get; set; }

    public DateOnly? PgpCurrentModifiedDate { get; set; }

    public decimal? PgpCurrentPid { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtParamValueGroup> CtParamValueGroups { get; set; } = new List<CtParamValueGroup>();

    public virtual CtParamHeader? PgpPhd { get; set; }
}
