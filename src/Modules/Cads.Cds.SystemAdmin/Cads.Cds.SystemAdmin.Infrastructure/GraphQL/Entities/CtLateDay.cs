using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLateDay
{
    public decimal? LdaVersion { get; set; }

    public decimal LdaId { get; set; }

    public char? LdaApplicType { get; set; }

    public DateOnly? LdaStartDate { get; set; }

    public decimal? LdaValidDays { get; set; }

    public string? LdaCurrentUser { get; set; }

    public DateOnly? LdaCurrentModifiedDate { get; set; }

    public decimal? LdaCurrentPid { get; set; }

    public string? LdaCurrentStatus { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
