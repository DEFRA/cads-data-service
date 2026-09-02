using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMhsToCph
{
    public string? Cph { get; set; }

    public decimal? MhsNumber { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
