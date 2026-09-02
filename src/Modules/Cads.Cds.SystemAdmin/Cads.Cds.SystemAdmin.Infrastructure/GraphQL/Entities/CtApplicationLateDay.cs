using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtApplicationLateDay
{
    public decimal AldId { get; set; }

    public decimal? AldValidDays { get; set; }

    public DateOnly? AldEffectiveFromDate { get; set; }

    public string? AldApplicationType { get; set; }

    public decimal? AldAdditionalDaysLate { get; set; }

    public string? AldCurrentUser { get; set; }

    public string? AldCurrentStatus { get; set; }

    public decimal? AldCurrentPid { get; set; }

    public DateOnly? AldCurrentModifiedDate { get; set; }

    public decimal? AldVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
