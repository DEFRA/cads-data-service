using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;

public partial class AnimalSpeciesProductionType
{
    public string Species { get; set; } = null!;

    public string ProductionType { get; set; } = null!;

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual AnimalProductionType ProductionTypeNavigation { get; set; } = null!;

    public virtual AnimalSpecy SpeciesNavigation { get; set; } = null!;
}