using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationIdFormat1
{
    public decimal? LifVersion { get; set; }

    public decimal LifId { get; set; }

    public string? LifSublocTypeReqd { get; set; }

    public string? LifDescription { get; set; }

    public string? LifLocTypeReqd { get; set; }

    public string? LifFormatPattern { get; set; }

    public string? LifCurrentUser { get; set; }

    public string? LifCurrentStatus { get; set; }

    public DateOnly? LifCurrentModifiedDate { get; set; }

    public decimal? LifCurrentPid { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? LifAudId { get; set; }

    public string? LifAudType { get; set; }

    public DateTime? LifAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
