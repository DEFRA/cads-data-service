using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalBreedState
{
    public string State { get; set; } = null!;

    public virtual ICollection<AnimalBreed> AnimalBreeds { get; set; } = new List<AnimalBreed>();
}