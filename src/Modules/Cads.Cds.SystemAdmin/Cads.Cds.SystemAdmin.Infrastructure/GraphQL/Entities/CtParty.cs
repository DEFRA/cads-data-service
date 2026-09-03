using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtParty
{
    public decimal ParId { get; set; }

    public string? ParInitials { get; set; }

    public string? ParSurname { get; set; }

    public string? ParTitle { get; set; }

    public string? ParWelshIndicator { get; set; }

    public string? ParEmailAddress { get; set; }

    public DateOnly? ParEffectiveFromDate { get; set; }

    public DateOnly? ParEffectiveToDate { get; set; }

    public string? ParFaxNumber { get; set; }

    public string? ParCessationReason { get; set; }

    public string? ParTelNumber { get; set; }

    public string? ParMobileNumber { get; set; }

    public string? ParComments { get; set; }

    public string? ParCurrentUser { get; set; }

    public string? ParCurrentStatus { get; set; }

    public DateOnly? ParCurrentModifiedDate { get; set; }

    public decimal? ParCurrentPid { get; set; }

    public decimal? ParVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtAddress> CtAddresses { get; set; } = new List<CtAddress>();

    public virtual ICollection<CtLocationPartyRel> CtLocationPartyRels { get; set; } = new List<CtLocationPartyRel>();
}