using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalDeathReason
{
    public string Species { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public virtual ICollection<AnimalCollectiveDeath> AnimalCollectiveDeaths { get; set; } = new List<AnimalCollectiveDeath>();

    public virtual ICollection<AnimalDeath> AnimalDeaths { get; set; } = new List<AnimalDeath>();

    public virtual AnimalSpecy SpeciesNavigation { get; set; } = null!;
}