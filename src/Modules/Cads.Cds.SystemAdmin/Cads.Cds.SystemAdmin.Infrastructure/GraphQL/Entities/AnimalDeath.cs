using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalDeath
{
    public string AnimalIdentifier { get; set; } = null!;

    public DateOnly DeathDate { get; set; }

    public DateOnly? DeathReportedDate { get; set; }

    public string DeathSiteIdentifier { get; set; } = null!;

    public string? DeathReasonSpecies { get; set; }

    public string? DeathReason { get; set; }

    public string? CarcassCollectionSiteIdentifier { get; set; }

    public bool TseTestRequiredFlag { get; set; }

    public DateOnly? DeathReceivedDate { get; set; }

    public virtual AnimalDeathReason? AnimalDeathReason { get; set; }

    public virtual Animal AnimalIdentifierNavigation { get; set; } = null!;

    public virtual AnimalSiteRef? CarcassCollectionSiteIdentifierNavigation { get; set; }

    public virtual AnimalSiteRef DeathSiteIdentifierNavigation { get; set; } = null!;
}
