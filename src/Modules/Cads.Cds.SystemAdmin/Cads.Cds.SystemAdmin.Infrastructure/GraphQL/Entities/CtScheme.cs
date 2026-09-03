using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtScheme
{
    public DateOnly? SchExpiryDate { get; set; }

    public string? SchShortDescription { get; set; }

    public decimal SchId { get; set; }

    public string? SchCurrentStatus { get; set; }

    public string? SchCurrentUser { get; set; }

    public DateOnly? SchCurrentModifiedDate { get; set; }

    public decimal? SchCurrentPid { get; set; }

    public string? SchScheme { get; set; }

    public string? SchLongDescription { get; set; }

    public decimal? SchVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtClaimType> CtClaimTypes { get; set; } = new List<CtClaimType>();
}