using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPpafGrouping2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal PpgId { get; set; }

    public decimal? PpgLocIdBirth { get; set; }

    public decimal? PpgLocIdCorres { get; set; }

    public string? PpgFormIdentifier { get; set; }

    public string? PpgWelshIndicator { get; set; }

    public string? PpgInterfaceFilename { get; set; }

    public decimal? PpgInterfaceTxnNumber { get; set; }

    public DateOnly? PpgPrintingDate { get; set; }

    public DateOnly? PpgPpafAddedDate { get; set; }

    public string? PpgCurrentStatus { get; set; }

    public string? PpgCurrentUser { get; set; }

    public DateOnly? PpgCurrentModifiedDate { get; set; }

    public decimal? PpgCurrentPid { get; set; }

    public decimal? PpgVersion { get; set; }

    public string? PpgCorresLocationRepd { get; set; }

    public string? PpgBirthLocationRepd { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtPpafGrouping1? AuditTrans { get; set; }
}
