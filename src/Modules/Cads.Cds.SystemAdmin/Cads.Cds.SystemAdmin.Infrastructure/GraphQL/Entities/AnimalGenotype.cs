using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalGenotype
{
    public string Species { get; set; } = null!;

    public string Genotype { get; set; } = null!;

    public virtual ICollection<AnimalCollectiveRegistration> AnimalCollectiveRegistrations { get; set; } = new List<AnimalCollectiveRegistration>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual AnimalSpecy SpeciesNavigation { get; set; } = null!;
}
