using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalLostOrStolenState
{
    public string State { get; set; } = null!;

    public virtual ICollection<AnimalLostOrStolenStatus> AnimalLostOrStolenStatuses { get; set; } = new List<AnimalLostOrStolenStatus>();
}
