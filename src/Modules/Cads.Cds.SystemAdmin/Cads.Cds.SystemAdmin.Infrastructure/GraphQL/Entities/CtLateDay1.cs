using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLateDay1
{
    public decimal? LdaVersion { get; set; }

    public decimal LdaId { get; set; }

    public string? LdaApplicType { get; set; }

    public DateOnly? LdaStartDate { get; set; }

    public decimal? LdaValidDays { get; set; }

    public string? LdaCurrentUser { get; set; }

    public DateOnly? LdaCurrentModifiedDate { get; set; }

    public decimal? LdaCurrentPid { get; set; }

    public string? LdaCurrentStatus { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? LdaAudId { get; set; }

    public string? LdaAudType { get; set; }

    public DateTime? LdaAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
