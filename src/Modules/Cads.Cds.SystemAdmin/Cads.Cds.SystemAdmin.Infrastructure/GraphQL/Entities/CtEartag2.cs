using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartag2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public decimal EtgId { get; set; }

    public decimal? EtgEttId { get; set; }

    public decimal? EtgErfId { get; set; }

    public string? EtgEartag { get; set; }

    public string? EtgUsageCode { get; set; }

    public string? EtgEartagAuthority { get; set; }

    public string? EtgSource { get; set; }

    public string? EtgIdentifierAvailability { get; set; }

    public string? EtgSpecies { get; set; }

    public string? EtgFuzzyEartag1 { get; set; }

    public string? EtgFuzzyEartag2 { get; set; }

    public string? EtgEartagDefraFormat { get; set; }

    public string? EtgTypeDefraFormat { get; set; }

    public string? EtgCurrentUser { get; set; }

    public DateOnly? EtgCurrentModifiedDate { get; set; }

    public string? EtgCurrentStatus { get; set; }

    public decimal? EtgCurrentPid { get; set; }

    public decimal? EtgVersion { get; set; }

    public decimal? EtgLocIdOrder { get; set; }

    public string? EtgOrderLocationRepd { get; set; }

    public string? EtgPpafIndicator { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtEartag1? AuditTrans { get; set; }
}
