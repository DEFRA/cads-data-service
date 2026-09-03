using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWorkgroup1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal WgpId { get; set; }

    public string? WgpWorkgroup { get; set; }

    public string? WgpShortName { get; set; }

    public string? WgpLongName { get; set; }

    public string? WgpActiveIndicator { get; set; }

    public string? WgpPrinter { get; set; }

    public string? WgpSummaryType { get; set; }

    public string? WgpReassignLock { get; set; }

    public string? WgpCurrentStatus { get; set; }

    public DateOnly? WgpCurrentModifiedDate { get; set; }

    public string? WgpCurrentUser { get; set; }

    public decimal? WgpCurrentPid { get; set; }

    public decimal? WgpVersion { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public long? WgpAudId { get; set; }

    public string? WgpAudType { get; set; }

    public DateTime? WgpAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtWorkgroup2> CtWorkgroup2s { get; set; } = new List<CtWorkgroup2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}