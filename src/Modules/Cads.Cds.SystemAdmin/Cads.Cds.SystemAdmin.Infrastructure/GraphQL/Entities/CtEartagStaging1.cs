using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagStaging1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal EstId { get; set; }

    public string? EstEartag { get; set; }

    public string? EstUsageCode { get; set; }

    public string? EstIdentifierAvailability { get; set; }

    public string? EstOrderLocationRepd { get; set; }

    public decimal? EstLocIdOrder { get; set; }

    public string? EstEartagReasonCode { get; set; }

    public decimal? EstErfId { get; set; }

    public DateOnly? EstCurrentModifiedDate { get; set; }

    public long? EstAudId { get; set; }

    public string? EstAudType { get; set; }

    public DateTime? EstAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtEartagStaging2> CtEartagStaging2s { get; set; } = new List<CtEartagStaging2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}