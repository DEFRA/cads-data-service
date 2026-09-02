using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtExtSpecialHerd
{
    public string? SphHerdCode { get; set; }

    public string? SphHerdRegion { get; set; }

    public decimal? SphVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
