using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPs9999AhdbMovHistory
{
    public decimal? RanId { get; set; }

    public DateOnly? OnDate { get; set; }

    public DateOnly? OffDate { get; set; }

    public decimal? LocId { get; set; }

    public string? LocFullIdentifier { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}