using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalPartyRef
{
    public int Identifier { get; set; }

    public virtual ICollection<AnimalCollectiveParty> AnimalCollectiveParties { get; set; } = new List<AnimalCollectiveParty>();

    public virtual ICollection<AnimalParty> AnimalParties { get; set; } = new List<AnimalParty>();
}
