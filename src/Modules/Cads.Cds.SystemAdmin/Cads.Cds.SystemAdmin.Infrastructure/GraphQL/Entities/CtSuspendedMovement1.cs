using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspendedMovement1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal SmoId { get; set; }

    public string? SmoCurrentUser { get; set; }

    public string? SmoCurrentStatus { get; set; }

    public DateOnly? SmoCurrentModifiedDate { get; set; }

    public decimal? SmoCurrentPid { get; set; }

    public string? SmoSourceType { get; set; }

    public decimal? SmoMovementType { get; set; }

    public DateOnly? SmoMovementDate { get; set; }

    public DateOnly? SmoMovementReceivedDate { get; set; }

    public string? SmoEartag { get; set; }

    public string? SmoOriginator { get; set; }

    public DateOnly? SmoSuspenseDate { get; set; }

    public string? SmoDirection { get; set; }

    public string? SmoMovementLocType { get; set; }

    public string? SmoMovementLocIdentifier { get; set; }

    public string? SmoMovementSublocIdentifier { get; set; }

    public string? SmoOriginatorsReference { get; set; }

    public string? SmoKillNumber { get; set; }

    public string? SmoEidReported { get; set; }

    public string? SmoMovtWorkgroup { get; set; }

    public string? SmoSuspenseReason { get; set; }

    public string? SmoCurrentPurposeCode { get; set; }

    public string? SmoInterfaceFileName { get; set; }

    public decimal? SmoInterfaceFileTxn { get; set; }

    public string? SmoOrigInterfaceFileName { get; set; }

    public decimal? SmoOrigInterfaceFileTxn { get; set; }

    public DateOnly? SmoSubmitDatetime { get; set; }

    public string? SmoAmendedBy { get; set; }

    public DateOnly? SmoAmendedDatetime { get; set; }

    public string? SmoAmendmentReason { get; set; }

    public decimal? SmoVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? SmoAudId { get; set; }

    public string? SmoAudType { get; set; }

    public DateTime? SmoAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtSuspendedMovement2> CtSuspendedMovement2s { get; set; } = new List<CtSuspendedMovement2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
