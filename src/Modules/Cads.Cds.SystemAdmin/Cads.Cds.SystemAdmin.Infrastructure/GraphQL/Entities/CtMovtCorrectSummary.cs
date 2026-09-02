using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMovtCorrectSummary
{
    public decimal McsId { get; set; }

    public string? McsCurrentUser { get; set; }

    public string? McsCurrentStatus { get; set; }

    public DateOnly? McsCurrentModifiedDate { get; set; }

    public decimal? McsCurrentPid { get; set; }

    public string? McsSmoOrRmoInd { get; set; }

    public decimal? McsSmoId { get; set; }

    public decimal? McsRmoId { get; set; }

    public decimal? McsMovId { get; set; }

    public string? McsSourceType { get; set; }

    public DateOnly? McsSuspenseDatetime { get; set; }

    public string? McsOrigInterfaceFileName { get; set; }

    public decimal? McsOrigInterfaceFileTxn { get; set; }

    public string? McsInterfaceFileName { get; set; }

    public decimal? McsInterfaceFileTxn { get; set; }

    public string? McsInitEartag { get; set; }

    public string? McsInitLocType { get; set; }

    public string? McsInitLocIdentifier { get; set; }

    public string? McsInitSublocIdentifier { get; set; }

    public string? McsInitMovementType { get; set; }

    public string? McsInitMovementDate { get; set; }

    public string? McsInitMovementRcvdDate { get; set; }

    public string? McsInitOriginator { get; set; }

    public string? McsInitOriginatorsReference { get; set; }

    public string? McsInitEidReported { get; set; }

    public string? McsInitKillNumber { get; set; }

    public string? McsInitWorkgroup { get; set; }

    public string? McsInitSuspenseReason { get; set; }

    public string? McsInitPurposeCode { get; set; }

    public string? McsSubmitAmendmentReason { get; set; }

    public string? McsSubmitWorkgroup { get; set; }

    public string? McsSubmitUser { get; set; }

    public DateOnly? McsSubmitDate { get; set; }

    public string? McsSubmitStatus { get; set; }

    public string? McsSubmitPurposeCode { get; set; }

    public decimal? McsVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtMovtCorrSummError> CtMovtCorrSummErrors { get; set; } = new List<CtMovtCorrSummError>();

    public virtual CtRegisteredMovement? McsMov { get; set; }

    public virtual CtReceivedMovement? McsRmo { get; set; }

    public virtual CtSuspendedMovement? McsSmo { get; set; }
}
