using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPreprintedAppnForm1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal PafId { get; set; }

    public decimal? PafEtgId { get; set; }

    public decimal? PafPpgId { get; set; }

    public string? PafReasonForIssue { get; set; }

    public decimal? PafInterfaceTxnNumber { get; set; }

    public string? PafInterfaceFilename { get; set; }

    public DateOnly? PafDateIssued { get; set; }

    public string? PafCurrentStatus { get; set; }

    public DateOnly? PafCurrentModifiedDate { get; set; }

    public string? PafCurrentUser { get; set; }

    public decimal? PafCurrentPid { get; set; }

    public decimal? PafVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? PafAudId { get; set; }

    public string? PafAudType { get; set; }

    public DateTime? PafAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtPreprintedAppnForm2> CtPreprintedAppnForm2s { get; set; } = new List<CtPreprintedAppnForm2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}