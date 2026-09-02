using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtParamGroup1
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

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? PgpAudId { get; set; }

    public string? PgpAudType { get; set; }

    public DateTime? PgpAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
