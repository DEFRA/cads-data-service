using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalCollectiveRole
{
    public string Role { get; set; } = null!;

    public virtual ICollection<AnimalCollectiveParty> AnimalCollectiveParties { get; set; } = new List<AnimalCollectiveParty>();
}