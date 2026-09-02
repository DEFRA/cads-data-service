using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationIdentifier1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal LidId { get; set; }

    public decimal? LidLocId { get; set; }

    public DateOnly? LidEffectiveFromDate { get; set; }

    public string? LidIdentifier { get; set; }

    public string? LidFullIdentifier { get; set; }

    public string? LidSubIdentifier { get; set; }

    public DateOnly? LidEffectiveToDate { get; set; }

    public string? LidCurrentStatus { get; set; }

    public DateOnly? LidCurrentModifiedDate { get; set; }

    public string? LidCurrentUser { get; set; }

    public decimal? LidCurrentPid { get; set; }

    public string? LidCurrentAmendReason { get; set; }

    public decimal? LidVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? LidAudId { get; set; }

    public string? LidAudType { get; set; }

    public DateTime? LidAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtLocationIdentifier2> CtLocationIdentifier2s { get; set; } = new List<CtLocationIdentifier2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
