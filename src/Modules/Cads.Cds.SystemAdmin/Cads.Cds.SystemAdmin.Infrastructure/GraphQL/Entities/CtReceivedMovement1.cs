using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtReceivedMovement1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal RmoId { get; set; }

    public string? RmoCurrentUser { get; set; }

    public string? RmoCurrentStatus { get; set; }

    public DateOnly? RmoCurrentModifiedDate { get; set; }

    public decimal? RmoCurrentPid { get; set; }

    public string? RmoSourceType { get; set; }

    public string? RmoSuspenseReason { get; set; }

    public string? RmoDirection { get; set; }

    public string? RmoEartag { get; set; }

    public string? RmoMovementDate { get; set; }

    public string? RmoMovementType { get; set; }

    public string? RmoMovementReceivedDate { get; set; }

    public string? RmoMovementLocType { get; set; }

    public string? RmoMovementLocIdentifier { get; set; }

    public string? RmoMovementSublocIdentifier { get; set; }

    public string? RmoLocFullIdentifier { get; set; }

    public string? RmoOriginator { get; set; }

    public string? RmoOriginatorsReference { get; set; }

    public string? RmoKillNumber { get; set; }

    public string? RmoEidReported { get; set; }

    public string? RmoMovtWorkgroup { get; set; }

    public string? RmoInterfaceFileName { get; set; }

    public decimal? RmoInterfaceFileTxn { get; set; }

    public string? RmoOrigInterfaceFileName { get; set; }

    public decimal? RmoOrigInterfaceFileTxn { get; set; }

    public DateOnly? RmoCreatedDate { get; set; }

    public DateOnly? RmoSubmitDatetime { get; set; }

    public DateOnly? RmoAmendedDatetime { get; set; }

    public string? RmoAmendedBy { get; set; }

    public string? RmoAmendmentReason { get; set; }

    public decimal? RmoVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? RmoAudId { get; set; }

    public string? RmoAudType { get; set; }

    public DateTime? RmoAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtReceivedMovement2> CtReceivedMovement2s { get; set; } = new List<CtReceivedMovement2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}