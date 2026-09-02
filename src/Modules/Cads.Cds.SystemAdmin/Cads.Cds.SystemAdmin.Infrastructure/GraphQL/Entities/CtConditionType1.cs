using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtConditionType1
{
    public decimal CotId { get; set; }

    public string? CotConditionType { get; set; }

    public string? CotShortDescription { get; set; }

    public DateOnly? CotEffectiveFromDate { get; set; }

    public string? CotLongDescription { get; set; }

    public DateOnly? CotEffectiveToDate { get; set; }

    public string? CotCessationReason { get; set; }

    public string? CotAccessGroup { get; set; }

    public string? CotCurrentUser { get; set; }

    public string? CotCurrentStatus { get; set; }

    public DateOnly? CotCurrentModifiedDate { get; set; }

    public decimal? CotCurrentPid { get; set; }

    public decimal? CotVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? CotAudId { get; set; }

    public string? CotAudType { get; set; }

    public DateTime? CotAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
