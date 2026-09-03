using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtClaimStatus1
{
    public decimal ClsId { get; set; }

    public decimal? ClsCurrentPid { get; set; }

    public string? ClsCurrentStatus { get; set; }

    public string? ClsCurrentUser { get; set; }

    public DateOnly? ClsCurrentModifiedDate { get; set; }

    public string? ClsClaimStatus { get; set; }

    public string? ClsDescription { get; set; }

    public decimal? ClsVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? ClsAudId { get; set; }

    public string? ClsAudType { get; set; }

    public DateTime? ClsAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}