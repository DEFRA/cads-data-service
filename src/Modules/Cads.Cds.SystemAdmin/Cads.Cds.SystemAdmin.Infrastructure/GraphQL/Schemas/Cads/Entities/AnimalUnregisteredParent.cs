using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;

public partial class AnimalUnregisteredParent
{
    public string AnimalIdentifier { get; set; } = null!;

    public string? SireAnimalIdentifier { get; set; }

    public string? GeneticDamAnimalIdentifier { get; set; }

    public string? BirthDamAnimalIdentifier { get; set; }

    public virtual Animal AnimalIdentifierNavigation { get; set; } = null!;
}
