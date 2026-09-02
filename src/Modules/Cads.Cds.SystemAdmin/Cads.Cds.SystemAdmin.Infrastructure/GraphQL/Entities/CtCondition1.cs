using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCondition1
{
    public decimal ConId { get; set; }

    public decimal? ConPchId { get; set; }

    public decimal? ConCotId { get; set; }

    public decimal? ConCurrentPid { get; set; }

    public string? ConReportRecipient { get; set; }

    public string? ConLongDescription { get; set; }

    public string? ConAllocationProcess { get; set; }

    public string? ConShortDescription { get; set; }

    public string? ConScope { get; set; }

    public string? ConCurrentUser { get; set; }

    public string? ConCurrentStatus { get; set; }

    public DateOnly? ConCurrentModifiedDate { get; set; }

    public string? ConConditionCode { get; set; }

    public decimal? ConVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? ConAudId { get; set; }

    public string? ConAudType { get; set; }

    public DateTime? ConAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
