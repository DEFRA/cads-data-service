using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalCollectiveDeath
{
    public int Identifier { get; set; }

    public string Species { get; set; } = null!;

    public string SiteIdentifier { get; set; } = null!;

    public int Quantity { get; set; }

    public DateOnly DeathDate { get; set; }

    public string? DeathReasonSpecies { get; set; }

    public string? DeathReason { get; set; }

    public string? CarcassCollectionSiteIdentifier { get; set; }

    public string? MarkSpecies { get; set; }

    public string? MarkCollectiveSiteIdentifier { get; set; }

    public string? Mark { get; set; }

    public virtual AnimalCollectiveRef AnimalCollectiveRef { get; set; } = null!;

    public virtual AnimalDeathReason? AnimalDeathReason { get; set; }

    public virtual AnimalMark? AnimalMark { get; set; }

    public virtual AnimalSiteRef? CarcassCollectionSiteIdentifierNavigation { get; set; }
}