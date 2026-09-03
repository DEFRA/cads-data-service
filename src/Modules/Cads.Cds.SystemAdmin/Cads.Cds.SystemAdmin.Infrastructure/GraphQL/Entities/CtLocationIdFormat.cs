using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationIdFormat
{
    public decimal? LifVersion { get; set; }

    public decimal LifId { get; set; }

    public string? LifSublocTypeReqd { get; set; }

    public string? LifDescription { get; set; }

    public string? LifLocTypeReqd { get; set; }

    public string? LifFormatPattern { get; set; }

    public string? LifCurrentUser { get; set; }

    public string? LifCurrentStatus { get; set; }

    public DateOnly? LifCurrentModifiedDate { get; set; }

    public decimal? LifCurrentPid { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtLocationType> CtLocationTypes { get; set; } = new List<CtLocationType>();
}