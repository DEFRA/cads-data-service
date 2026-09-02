using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtClaimType1
{
    public decimal CltId { get; set; }

    public decimal? CltCurrentPid { get; set; }

    public string? CltCurrentStatus { get; set; }

    public string? CltCurrentUser { get; set; }

    public DateOnly? CltCurrentModifiedDate { get; set; }

    public decimal? CltSchId { get; set; }

    public string? CltClaimType { get; set; }

    public string? CltDescription { get; set; }

    public decimal? CltVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? CltAudId { get; set; }

    public string? CltAudType { get; set; }

    public DateTime? CltAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
