using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtValidApplication1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal VapId { get; set; }

    public string? VapCurrentStatus { get; set; }

    public string? VapCurrentUser { get; set; }

    public DateOnly? VapCurrentModifiedDate { get; set; }

    public decimal? VapCurrentPid { get; set; }

    public string? VapCurrentIntendedAction { get; set; }

    public string? VapApplicationType { get; set; }

    public DateOnly? VapReceiptDate { get; set; }

    public decimal? VapLocIdRequester { get; set; }

    public DateOnly? VapRequesterDate { get; set; }

    public string? VapCountyRequester { get; set; }

    public string? VapSourceType { get; set; }

    public DateOnly? VapTargetDate { get; set; }

    public string? VapSourceReference { get; set; }

    public string? VapCtsIndicator { get; set; }

    public decimal? VapNoOfAnimals { get; set; }

    public decimal? VapNoOfAnimalsNotCanc { get; set; }

    public decimal? VapNumberCalfMovts { get; set; }

    public string? VapInterfaceFileName { get; set; }

    public decimal? VapInterfaceFileTxn { get; set; }

    public decimal? VapWurId { get; set; }

    public decimal? VapVersion { get; set; }

    public string? VapRequesterLocationRepd { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public long? VapAudId { get; set; }

    public string? VapAudType { get; set; }

    public DateTime? VapAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtValidApplication2> CtValidApplication2s { get; set; } = new List<CtValidApplication2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}