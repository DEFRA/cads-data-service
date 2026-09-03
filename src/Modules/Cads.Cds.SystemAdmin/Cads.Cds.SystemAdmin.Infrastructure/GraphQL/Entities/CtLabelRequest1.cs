using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLabelRequest1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal LarId { get; set; }

    public decimal? LarLasId { get; set; }

    public decimal? LarSheetQuantity { get; set; }

    public string? LarLabelType { get; set; }

    public decimal? LarLabelVersion { get; set; }

    public DateOnly? LarSubmittedDate { get; set; }

    public DateOnly? LarRequestedDate { get; set; }

    public string? LarReasonCode { get; set; }

    public string? LarPrintMethod { get; set; }

    public string? LarLabelsInterfaceFile { get; set; }

    public string? LarKeeperTitle { get; set; }

    public string? LarKeeperInitials { get; set; }

    public string? LarKeeperSurname { get; set; }

    public string? LarLabelLocType { get; set; }

    public string? LarLabelLocIdentifier { get; set; }

    public string? LarLabelSublocIdentifier { get; set; }

    public string? LarLabelLocName { get; set; }

    public string? LarLabelAddress2 { get; set; }

    public string? LarLabelAddress3 { get; set; }

    public string? LarLabelAddress4 { get; set; }

    public string? LarLabelAddress5 { get; set; }

    public string? LarLabelPostCode { get; set; }

    public string? LarCorrLocType { get; set; }

    public string? LarCorrLocIdentifier { get; set; }

    public string? LarCorrSublocIdentifier { get; set; }

    public string? LarCorrTitle { get; set; }

    public string? LarCorrInitials { get; set; }

    public string? LarCorrSurname { get; set; }

    public string? LarCorrLocName { get; set; }

    public string? LarCorrAddress2 { get; set; }

    public string? LarCorrAddress3 { get; set; }

    public string? LarCorrAddress4 { get; set; }

    public string? LarCorrAddress5 { get; set; }

    public string? LarCorrPostCode { get; set; }

    public string? LarCurrentUser { get; set; }

    public string? LarCurrentStatus { get; set; }

    public DateOnly? LarCurrentModifiedDate { get; set; }

    public decimal? LarCurrentPid { get; set; }

    public decimal? LarVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? LarAudId { get; set; }

    public string? LarAudType { get; set; }

    public DateTime? LarAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtLabelRequest2> CtLabelRequest2s { get; set; } = new List<CtLabelRequest2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}