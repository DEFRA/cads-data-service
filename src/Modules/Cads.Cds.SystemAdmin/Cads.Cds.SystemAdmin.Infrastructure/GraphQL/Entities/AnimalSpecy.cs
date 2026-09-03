using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalSpecy
{
    public string Species { get; set; } = null!;

    public virtual ICollection<AnimalBreed> AnimalBreeds { get; set; } = new List<AnimalBreed>();

    public virtual ICollection<AnimalCollectiveRef> AnimalCollectiveRefs { get; set; } = new List<AnimalCollectiveRef>();

    public virtual ICollection<AnimalDeathReason> AnimalDeathReasons { get; set; } = new List<AnimalDeathReason>();

    public virtual ICollection<AnimalGenotype> AnimalGenotypes { get; set; } = new List<AnimalGenotype>();

    public virtual ICollection<AnimalNoticeToIdentify> AnimalNoticeToIdentifies { get; set; } = new List<AnimalNoticeToIdentify>();

    public virtual ICollection<AnimalSpeciesProductionType> AnimalSpeciesProductionTypes { get; set; } = new List<AnimalSpeciesProductionType>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}