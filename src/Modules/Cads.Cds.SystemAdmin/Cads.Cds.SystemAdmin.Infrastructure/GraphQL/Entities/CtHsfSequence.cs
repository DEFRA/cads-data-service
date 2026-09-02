using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtHsfSequence
{
    public string? HssSequenceKey { get; set; }

    public decimal? HssSequence { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
