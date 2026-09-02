using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class Animal
{
    public string Identifier { get; set; } = null!;

    public string? AnimalIdentifierIdentifier { get; set; }

    public string? OriginalIdentifier { get; set; }

    public string Species { get; set; } = null!;

    public string? BreedSpecies { get; set; }

    public string? BreedCode { get; set; }

    public string? GenotypeSpecies { get; set; }

    public string? Genotype { get; set; }

    public string? Name { get; set; }

    public string Sex { get; set; } = null!;

    public string? ProductionType { get; set; }

    public DateOnly? IdentificationDate { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public string? GeneticDamIdentifier { get; set; }

    public string? SireIdentifier { get; set; }

    public string? BirthDamIdentifier { get; set; }

    public string RegistrationSiteIdentifier { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public string RegistrationCategory { get; set; } = null!;

    public virtual AnimalBirth? AnimalBirth { get; set; }

    public virtual AnimalBreed? AnimalBreed { get; set; }

    public virtual ICollection<AnimalCollective> AnimalCollectives { get; set; } = new List<AnimalCollective>();

    public virtual AnimalDeath? AnimalDeath { get; set; }

    public virtual AnimalGenotype? AnimalGenotype { get; set; }

    public virtual AnimalIdentifier? AnimalIdentifierIdentifierNavigation { get; set; }

    public virtual ICollection<AnimalLostOrStolenStatus> AnimalLostOrStolenStatuses { get; set; } = new List<AnimalLostOrStolenStatus>();

    public virtual ICollection<AnimalNoticeToIdentify> AnimalNoticeToIdentifyAnimalIdentifierNavigations { get; set; } = new List<AnimalNoticeToIdentify>();

    public virtual ICollection<AnimalNoticeToIdentify> AnimalNoticeToIdentifyOriginalAnimalIdentifierNavigations { get; set; } = new List<AnimalNoticeToIdentify>();

    public virtual ICollection<AnimalParty> AnimalParties { get; set; } = new List<AnimalParty>();

    public virtual AnimalSpeciesProductionType? AnimalSpeciesProductionType { get; set; }

    public virtual ICollection<AnimalStatus> AnimalStatuses { get; set; } = new List<AnimalStatus>();

    public virtual AnimalUnregisteredParent? AnimalUnregisteredParent { get; set; }

    public virtual Animal? BirthDamIdentifierNavigation { get; set; }

    public virtual Animal? GeneticDamIdentifierNavigation { get; set; }

    public virtual ICollection<Animal> InverseBirthDamIdentifierNavigation { get; set; } = new List<Animal>();

    public virtual ICollection<Animal> InverseGeneticDamIdentifierNavigation { get; set; } = new List<Animal>();

    public virtual ICollection<Animal> InverseSireIdentifierNavigation { get; set; } = new List<Animal>();

    public virtual AnimalRegistrationCategory RegistrationCategoryNavigation { get; set; } = null!;

    public virtual AnimalSiteRef RegistrationSiteIdentifierNavigation { get; set; } = null!;

    public virtual AnimalSex SexNavigation { get; set; } = null!;

    public virtual Animal? SireIdentifierNavigation { get; set; }

    public virtual AnimalSpecy SpeciesNavigation { get; set; } = null!;
}
