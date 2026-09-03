using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalSiteRef
{
    public string Identifier { get; set; } = null!;

    public virtual ICollection<AnimalBirth> AnimalBirths { get; set; } = new List<AnimalBirth>();

    public virtual ICollection<AnimalCollectiveDeath> AnimalCollectiveDeaths { get; set; } = new List<AnimalCollectiveDeath>();

    public virtual ICollection<AnimalCollectiveRef> AnimalCollectiveRefs { get; set; } = new List<AnimalCollectiveRef>();

    public virtual ICollection<AnimalDeath> AnimalDeathCarcassCollectionSiteIdentifierNavigations { get; set; } = new List<AnimalDeath>();

    public virtual ICollection<AnimalDeath> AnimalDeathDeathSiteIdentifierNavigations { get; set; } = new List<AnimalDeath>();

    public virtual ICollection<AnimalLostOrStolenStatus> AnimalLostOrStolenStatuses { get; set; } = new List<AnimalLostOrStolenStatus>();

    public virtual ICollection<AnimalNoticeToIdentify> AnimalNoticeToIdentifies { get; set; } = new List<AnimalNoticeToIdentify>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}