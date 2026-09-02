using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalBreed
{
    public string Species { get; set; } = null!;

    public string BreedCode { get; set; } = null!;

    public string? Breed { get; set; }

    public bool CrossBreedFlag { get; set; }

    public string State { get; set; } = null!;

    public virtual ICollection<AnimalCollectiveRegistration> AnimalCollectiveRegistrations { get; set; } = new List<AnimalCollectiveRegistration>();

    public virtual ICollection<AnimalNoticeToIdentify> AnimalNoticeToIdentifies { get; set; } = new List<AnimalNoticeToIdentify>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual AnimalSpecy SpeciesNavigation { get; set; } = null!;

    public virtual AnimalBreedState StateNavigation { get; set; } = null!;
}
