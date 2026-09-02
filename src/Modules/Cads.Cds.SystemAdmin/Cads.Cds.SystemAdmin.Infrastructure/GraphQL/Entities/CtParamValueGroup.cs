using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtParamValueGroup
{
    public decimal PvgId { get; set; }

    public decimal? PvgPgpId { get; set; }

    public decimal? PvgPvlId { get; set; }

    public string? PvgGroupValue { get; set; }

    public string? PvgParam { get; set; }

    public string? PvgParamValue { get; set; }

    public string? PvgCurrentUser { get; set; }

    public string? PvgCurrentStatus { get; set; }

    public DateOnly? PvgCurrentModifiedDate { get; set; }

    public decimal? PvgCurrentPid { get; set; }

    public decimal? PvgVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtParamGroup? PvgPgp { get; set; }

    public virtual CtParamValue? PvgPvl { get; set; }
}
