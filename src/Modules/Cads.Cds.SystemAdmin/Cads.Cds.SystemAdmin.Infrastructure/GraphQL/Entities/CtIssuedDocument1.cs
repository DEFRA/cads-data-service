using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtIssuedDocument1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal IdoId { get; set; }

    public decimal? IdoLocId { get; set; }

    public DateOnly? IdoCreationDate { get; set; }

    public string? IdoReasonCode { get; set; }

    public string? IdoInterfaceFileName { get; set; }

    public string? IdoPassptLayoutVerNumber { get; set; }

    public decimal? IdoInterfaceTxnNumber { get; set; }

    public decimal? IdoPassportVersionNumber { get; set; }

    public string? IdoCurrentStatus { get; set; }

    public DateOnly? IdoCurrentModifiedDate { get; set; }

    public string? IdoCurrentUser { get; set; }

    public decimal? IdoCurrentPid { get; set; }

    public decimal? IdoRanId { get; set; }

    public decimal? IdoVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? IdoAudId { get; set; }

    public string? IdoAudType { get; set; }

    public DateTime? IdoAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtIssuedDocument2> CtIssuedDocument2s { get; set; } = new List<CtIssuedDocument2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
