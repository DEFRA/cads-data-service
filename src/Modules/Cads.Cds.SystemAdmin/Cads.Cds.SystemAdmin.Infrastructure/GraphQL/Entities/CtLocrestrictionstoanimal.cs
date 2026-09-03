using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocrestrictionstoanimal
{
    public decimal? LraComId { get; set; }

    public DateOnly? LraLastProbityDate { get; set; }

    public DateOnly? LraComEffectiveFrom { get; set; }

    public DateOnly? LraComEffectiveTo { get; set; }

    public decimal? LraLocId { get; set; }

    public decimal? LraRanId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtConditionMarker? LraCom { get; set; }

    public virtual CtLocation? LraLoc { get; set; }

    public virtual CtRegisteredAnimal? LraRan { get; set; }
}