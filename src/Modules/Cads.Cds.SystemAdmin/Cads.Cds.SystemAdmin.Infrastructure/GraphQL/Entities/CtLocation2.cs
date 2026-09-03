using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocation2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public string? LocReceivePpafFlag { get; set; }

    public decimal LocId { get; set; }

    public decimal? LocSltId { get; set; }

    public decimal? LocLtyId { get; set; }

    public decimal? LocCtyId { get; set; }

    public string? LocReceiveLabelsFlag { get; set; }

    public DateOnly? LocEffectiveFrom { get; set; }

    public DateOnly? LocEffectiveTo { get; set; }

    public string? LocCessationReason { get; set; }

    public string? LocPremisesType { get; set; }

    public string? LocComments { get; set; }

    public string? LocMapReference { get; set; }

    public string? LocSourceIdentifier { get; set; }

    public string? LocSourceReference { get; set; }

    public string? LocTelNumber { get; set; }

    public string? LocMobileNumber { get; set; }

    public string? LocFaxNumber { get; set; }

    public string? LocEmailAddress { get; set; }

    public string? LocCurrentStatus { get; set; }

    public string? LocCurrentUser { get; set; }

    public DateOnly? LocCurrentModifiedDate { get; set; }

    public decimal? LocCurrentPid { get; set; }

    public string? LocReasonCode { get; set; }

    public decimal? LocVersion { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtLocation1? AuditTrans { get; set; }
}