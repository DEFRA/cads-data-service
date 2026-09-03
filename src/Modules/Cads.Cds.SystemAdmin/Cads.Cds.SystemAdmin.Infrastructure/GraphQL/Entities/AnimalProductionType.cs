using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalProductionType
{
    public string Type { get; set; } = null!;

    public virtual ICollection<AnimalSpeciesProductionType> AnimalSpeciesProductionTypes { get; set; } = new List<AnimalSpeciesProductionType>();
}