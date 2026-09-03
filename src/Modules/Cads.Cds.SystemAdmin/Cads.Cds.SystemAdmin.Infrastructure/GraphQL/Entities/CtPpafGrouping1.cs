using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPpafGrouping1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

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

    public long? PpgAudId { get; set; }

    public string? PpgAudType { get; set; }

    public DateTime? PpgAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtPpafGrouping2> CtPpafGrouping2s { get; set; } = new List<CtPpafGrouping2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}