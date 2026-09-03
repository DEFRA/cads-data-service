using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalCollectiveRegistration
{
    public int Identifier { get; set; }

    public string Species { get; set; } = null!;

    public string SiteIdentifier { get; set; } = null!;

    public int Quantity { get; set; }

    public string BirthYear { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public DateOnly IdentificationDate { get; set; }

    public string? GenotypeSpecies { get; set; }

    public string? Genotype { get; set; }

    public string? MarkSpecies { get; set; }

    public string? MarkCollectiveSiteIdentifier { get; set; }

    public string? Mark { get; set; }

    public string? BreedSpecies { get; set; }

    public string? BreedCode { get; set; }

    public virtual AnimalBreed? AnimalBreed { get; set; }

    public virtual AnimalCollectiveRef AnimalCollectiveRef { get; set; } = null!;

    public virtual AnimalGenotype? AnimalGenotype { get; set; }

    public virtual AnimalMark? AnimalMark { get; set; }
}