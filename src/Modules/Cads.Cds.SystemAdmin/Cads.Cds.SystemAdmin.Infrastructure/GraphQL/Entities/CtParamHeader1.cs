using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtParamHeader1
{
    public decimal PhdId { get; set; }

    public string? PhdParam { get; set; }

    public string? PhdShortDesc { get; set; }

    public string? PhdLongDesc { get; set; }

    public string? PhdDontCache { get; set; }

    public string? PhdUseShort { get; set; }

    public string? PhdCurrentUser { get; set; }

    public string? PhdCurrentStatus { get; set; }

    public DateOnly? PhdCurrentModifiedDate { get; set; }

    public decimal? PhdCurrentPid { get; set; }

    public decimal? PhdVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? PhdAudId { get; set; }

    public string? PhdAudType { get; set; }

    public DateTime? PhdAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}