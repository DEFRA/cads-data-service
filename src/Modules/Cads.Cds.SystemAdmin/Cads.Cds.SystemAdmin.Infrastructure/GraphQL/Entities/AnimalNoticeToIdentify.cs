using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalNoticeToIdentify
{
    public string NoticeReference { get; set; } = null!;

    public string Species { get; set; } = null!;

    public string? AnimalIdentifier { get; set; }

    public bool DnaProvenFlag { get; set; }

    public string OriginalAnimalIdentifier { get; set; } = null!;

    public string OriginalAnimalIdentifierType { get; set; } = null!;

    public string? BreedSpecies { get; set; }

    public string? BreedCode { get; set; }

    public string Sex { get; set; } = null!;

    public DateOnly IssueDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? InspectionYear { get; set; }

    public string? AdditionalDetails { get; set; }

    public string? InspectionReference { get; set; }

    public string SiteIdentifier { get; set; } = null!;

    public string? Resolution { get; set; }

    public virtual AnimalBreed? AnimalBreed { get; set; }

    public virtual Animal? AnimalIdentifierNavigation { get; set; }

    public virtual Animal OriginalAnimalIdentifierNavigation { get; set; } = null!;

    public virtual AnimalOriginalIdentifierType OriginalAnimalIdentifierTypeNavigation { get; set; } = null!;

    public virtual AnimalResolutionType? ResolutionNavigation { get; set; }

    public virtual AnimalSex SexNavigation { get; set; } = null!;

    public virtual AnimalSiteRef SiteIdentifierNavigation { get; set; } = null!;

    public virtual AnimalSpecy SpeciesNavigation { get; set; } = null!;
}