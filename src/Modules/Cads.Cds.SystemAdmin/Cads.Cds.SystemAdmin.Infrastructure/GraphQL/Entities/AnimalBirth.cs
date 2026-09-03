using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalBirth
{
    public string AnimalIdentifier { get; set; } = null!;

    public string BirthSiteIdentifier { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string? BirthYear { get; set; }

    public string? BirthMarkSpecies { get; set; }

    public string? BirthMarkCollectiveSiteIdentifier { get; set; }

    public string? BirthMark { get; set; }

    public bool AssistedBirthFlag { get; set; }

    public bool MultipleBirthsFlag { get; set; }

    public bool EmbryoTransferFlag { get; set; }

    public virtual Animal AnimalIdentifierNavigation { get; set; } = null!;

    public virtual AnimalMark? AnimalMark { get; set; }

    public virtual AnimalSiteRef BirthSiteIdentifierNavigation { get; set; } = null!;
}