using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtNonWorkingDay1
{
    public decimal NwdId { get; set; }

    public DateOnly? NwdDate { get; set; }

    public string? NwdDescription { get; set; }

    public decimal? NwdYear { get; set; }

    public string? NwdCurrentUser { get; set; }

    public string? NwdCurrentStatus { get; set; }

    public DateOnly? NwdCurrentModifiedDate { get; set; }

    public decimal? NwdPid { get; set; }

    public decimal? NwdVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? NwdAudId { get; set; }

    public string? NwdAudType { get; set; }

    public DateTime? NwdAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}