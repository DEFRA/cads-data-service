using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalCollectiveRef
{
    public string Species { get; set; } = null!;

    public string SiteIdentifier { get; set; } = null!;

    public string State { get; set; } = null!;

    public virtual ICollection<AnimalCollective> AnimalCollectiveAnimalCollectiveRefNavigations { get; set; } = new List<AnimalCollective>();

    public virtual ICollection<AnimalCollective> AnimalCollectiveAnimalCollectiveRefs { get; set; } = new List<AnimalCollective>();

    public virtual ICollection<AnimalCollectiveDeath> AnimalCollectiveDeaths { get; set; } = new List<AnimalCollectiveDeath>();

    public virtual ICollection<AnimalCollectiveParty> AnimalCollectiveParties { get; set; } = new List<AnimalCollectiveParty>();

    public virtual ICollection<AnimalCollectiveRegistration> AnimalCollectiveRegistrations { get; set; } = new List<AnimalCollectiveRegistration>();

    public virtual ICollection<AnimalMark> AnimalMarks { get; set; } = new List<AnimalMark>();

    public virtual AnimalSiteRef SiteIdentifierNavigation { get; set; } = null!;

    public virtual AnimalSpecy SpeciesNavigation { get; set; } = null!;

    public virtual AnimalCollectiveState StateNavigation { get; set; } = null!;
}