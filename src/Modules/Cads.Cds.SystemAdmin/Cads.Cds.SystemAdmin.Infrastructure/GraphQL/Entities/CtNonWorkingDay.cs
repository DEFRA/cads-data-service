using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtNonWorkingDay
{
    public decimal NwdId { get; set; }

    public DateOnly? NwdDate { get; set; }

    public string? NwdDescription { get; set; }

    public decimal? NwdYear { get; set; }

    public string? NwdCurrentUser { get; set; }

    public string? NwdCurrentStatus { get; set; }

    public DateOnly? NwdCurrentModifiedDate { get; set; }

    public decimal? NwdPid { get; set; }

    public decimal? NwdVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}