using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspConditionMarker2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public decimal ScmId { get; set; }

    public decimal? ScmRanId { get; set; }

    public decimal? ScmLocId { get; set; }

    public string? ScmLocationType { get; set; }

    public DateOnly? ScmSubmitDate { get; set; }

    public DateOnly? ScmAmendmentDatetime { get; set; }

    public string? ScmAmendmentReason { get; set; }

    public string? ScmAmendmentReasonText { get; set; }

    public string? ScmAmendmentStatus { get; set; }

    public decimal? ScmOriginalInterfaceTxn { get; set; }

    public string? ScmConditionCode { get; set; }

    public string? ScmDocumentRefs { get; set; }

    public DateOnly? ScmEffectiveFromDate { get; set; }

    public string? ScmLocationIdentifier { get; set; }

    public string? ScmComments { get; set; }

    public string? ScmConditionVariant { get; set; }

    public DateOnly? ScmEffectiveToDate { get; set; }

    public string? ScmSuspenseReason { get; set; }

    public string? ScmSource { get; set; }

    public string? ScmOriginator { get; set; }

    public string? ScmConditionAuthority { get; set; }

    public string? ScmCurrentPurposeCode { get; set; }

    public string? ScmGroupingReference { get; set; }

    public string? ScmOriginalInterfaceFile { get; set; }

    public DateOnly? ScmCancellationDate { get; set; }

    public string? ScmConditionType { get; set; }

    public decimal? ScmInterfaceTxnNumber { get; set; }

    public string? ScmInterfaceFilename { get; set; }

    public string? ScmAddMatchFlag { get; set; }

    public string? ScmOwner { get; set; }

    public string? ScmAmendedBy { get; set; }

    public string? ScmAnimalIdentifier { get; set; }

    public string? ScmAnimalIdentifierType { get; set; }

    public string? ScmConditionActivity { get; set; }

    public string? ScmSublocationIdentifier { get; set; }

    public string? ScmUseType { get; set; }

    public string? ScmSystemError { get; set; }

    public string? ScmCurrentStatus { get; set; }

    public DateOnly? ScmCurrentModifiedDate { get; set; }

    public string? ScmCurrentUser { get; set; }

    public decimal? ScmCurrentPid { get; set; }

    public decimal? ScmVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtSuspConditionMarker1? AuditTrans { get; set; }
}
