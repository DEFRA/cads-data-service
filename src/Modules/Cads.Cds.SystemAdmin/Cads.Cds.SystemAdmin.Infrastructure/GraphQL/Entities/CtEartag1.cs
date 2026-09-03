using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartag1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

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

    public long? EtgAudId { get; set; }

    public string? EtgAudType { get; set; }

    public DateTime? EtgAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtEartag2> CtEartag2s { get; set; } = new List<CtEartag2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
